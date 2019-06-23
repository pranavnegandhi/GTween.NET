using GSkinner.Motion;
using GSkinner.Motion.Easing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace EasingDemos
{
    public partial class MainWindow : Form
    {
        private readonly ICollection<Type> _easings = new[] { typeof(Back), typeof(Bounce), typeof(Circular), typeof(Cubic), typeof(Elastic), typeof(Exponential), typeof(Linear), typeof(Quadratic), typeof(Quartic), typeof(Quintic), typeof(Sine) };

        private Point _plotFrom = new Point(0, 125);

        private Point _plotAt = new Point(0, 125);

        private Bitmap _bitmap;

        private GTween _xTween;

        private GTween _yTween;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var easing in _easings)
            {
                easings.Items.Add(easing);
            }

            _bitmap = new Bitmap(bitmapDisplay.Width, bitmapDisplay.Height);
            using (var gfx = Graphics.FromImage(_bitmap))
            {
                gfx.FillRectangle(Brushes.Black, 0, 0, _bitmap.Width, _bitmap.Height);
            }
            bitmapDisplay.Image = _bitmap;

            var values = new Dictionary<string, double>() { { "X", _bitmap.Width - 1 } };
            _xTween = new GTween(_plotAt, 5, values)
            {
                AutoPlay = false,
                DispatchEvents = false,
                Paused = true
            };

            values = new Dictionary<string, double>() { { "Y", _bitmap.Height - 1 } };
            _yTween = new GTween(_plotAt, 5, values)
            {
                AutoPlay = false,
                DispatchEvents = true,
                Paused = true
            };
            
            _yTween.Changed += TweenChanged;
            _yTween.Completed += TweenCompleted;
        }

        private void easings_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var gfx = Graphics.FromImage(_bitmap))
            {
                gfx.FillRectangle(Brushes.Black, 0, 0, _bitmap.Width, _bitmap.Height);
            }

            var easeType = easings.SelectedItem as Type;
            var metods = easeType.GetMethods(BindingFlags.Static | BindingFlags.Public);
            var easeMethod = metods[0].CreateDelegate(typeof(GTween.Easer));

            _plotFrom.X = _plotAt.X = 0;
            _plotFrom.Y = _plotAt.Y = 125;

            _xTween.Beginning();
            _yTween.Beginning();
            _xTween.Paused = _yTween.Paused = false;
            _yTween.Ease = (GTween.Easer)easeMethod;
        }
        
        private void TweenChanged(object sender, GTweenEventArgs e)
        {
            var x = Math.Max(_plotAt.X, 0);
            x = Math.Min(x, _bitmap.Width - 1);

            var y = _bitmap.Height - 1 - _plotAt.Y;
            y = Math.Max(y, 0);
            y = Math.Min(y, _bitmap.Height - 1);
            
            using (var gfx = Graphics.FromImage(_bitmap))
            {
                gfx.DrawLine(Pens.Red, (float)_plotFrom.X, (float)_plotFrom.Y, (float)x, (float)y);
            }

            bitmapDisplay.Invoke((MethodInvoker)bitmapDisplay.Refresh);

            _plotFrom.X = x;
            _plotFrom.Y = y;
        }

        private void TweenCompleted(object sender, GTweenEventArgs e)
        {
            _xTween.Paused = _yTween.Paused = true;
        }
    }
}