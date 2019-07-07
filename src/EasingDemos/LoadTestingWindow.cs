using GSkinner.Motion;
using GSkinner.Motion.Easing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EasingDemos
{
    public partial class LoadTestingWindow : Form
    {
        private GTween[] _tweens;

        private Bitmap _bitmap;

        private readonly object _bitmapDisplayPadlock = new { };

        private Graphics _graphics;

        private int _maxWidth;

        private int _maxHeight;

        private readonly Timer _refresh = new Timer();

        private int _completedCounter;

        public LoadTestingWindow()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _refresh.Interval = 40;
            _refresh.Tick += (s, e) => bitmapDisplay.Invalidate();
            _refresh.Enabled = false;
        }

        private void start_Click(object sender, EventArgs e)
        {
            _graphics.FillRectangle(Brushes.Black, 0, 0, _maxWidth, _maxHeight);

            var desiredCount = (int)instanceCounter.Value;
            _completedCounter = 0;

            _tweens = new GTween[desiredCount];
            GTween.PauseAll = true;
            for (var i = 0; i < desiredCount; i++)
            {
                var initializer = new GTweenPropertyInitializer()
                {
                    AutoPlay = false,
                    DispatchEvents = true
                };

                var target = new Point(0, i * 10);
                var values = new Dictionary<string, double>() { { "X", _maxWidth }, { "Y", _maxHeight + target.Y } };
                var tween = new GTween(target, 5, values, initializer);
                tween.Data = new Point(0, i * 10);
                tween.Ease = Sine.EaseInOut;
                tween.Completed += TweenCompleted;
                tween.Paused = false;
                _tweens[i] = tween;
            }
            GTween.PauseAll = false;

            _refresh.Enabled = true;
        }

        private void TweenCompleted(object sender, GTweenEventArgs e)
        {
            var tween = e.Tween;
            tween.Completed -= TweenCompleted;
            _completedCounter++;

            if (_refresh.Enabled && _completedCounter == _tweens.Length)
            {
                _refresh.Enabled = false;
            }
        }

        private void LoadTestingWindow_Load(object sender, EventArgs e)
        {
            _maxWidth = bitmapDisplay.Width - 1;
            _maxHeight = bitmapDisplay.Height - 1;

            _bitmap = new Bitmap(bitmapDisplay.Width, bitmapDisplay.Height);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.FillRectangle(Brushes.Black, 0, 0, _maxWidth, _maxHeight);
            bitmapDisplay.Image = _bitmap;

            bitmapDisplay.Paint += BitmapDisplay_Paint;
        }

        private void BitmapDisplay_Paint(object sender, PaintEventArgs e)
        {
            if (null != _tweens)
            {
                var count = _tweens.Length;

                for (var i = 0; i < count; i++)
                {
                    var tween = _tweens[i];
                    var from = (Point)tween.Data;
                    var to = (Point)tween.Target;

                    var yTo = Math.Max((float)to.Y, 0f);
                    yTo = Math.Min(yTo, _maxHeight);

                    var xFrom = Math.Max((float)from.X, 0f);
                    xFrom = Math.Min(xFrom, _maxWidth);

                    var yFrom = Math.Max((float)from.Y, 0f);
                    yFrom = Math.Min(yFrom, _maxHeight);

                    _graphics.DrawLine(Pens.Red, xFrom, yFrom, xFrom, yTo);

                    from.X += _maxWidth / (5000 / 40);
                    from.Y = to.Y;
                }
            }
        }
    }
}