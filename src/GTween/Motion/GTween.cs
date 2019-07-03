using GSkinner.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GSkinner.Motion
{
    /// <summary>
    /// <para>GTween.NET</para>
    /// <para>Based on GTween 2.01 for ActionScript 3 http://gskinner.com/libraries/gtween/</para>
    /// <para>This code is MIT licensed, see http://www.opensource.org/licenses/mit-license.php</para>
    /// <para>Copyright (c) 2019 Pranav Negandhi (http://www.notadesigner.com)</para>
    ///
    /// <para>GTween 2.01 for ActionScript 3 is MIT licensed, see http://www.opensource.org/licenses/mit-license.php</para>
    /// <para>Copyright(c) 2009 Grant Skinner</para>
    /// </summary>
    public class GTween
    {
        public event EventHandler<GTweenEventArgs> Changed;

        public event EventHandler<GTweenEventArgs> Completed;

        public event EventHandler<GTweenEventArgs> Initialized;

        public delegate double Easer(double a, double b, double c, double d);

        private bool _initialized;

        private IDictionary<string, double> _values;

        private IDictionary<string, double> _initValues;

        private IDictionary<string, double> _rangeValues;

        private double _delay;

        private bool _paused;

        private double _position;

        private static Thread _frameThread;

        private static HashSet<GTween> _instances = new HashSet<GTween>();

        private static readonly object _instancesPadlock = new { };

        private static CancellationTokenSource _cancelAnimation = new CancellationTokenSource();

        private static EventWaitHandle _playAnimation = new ManualResetEvent(true);

        private static DateTime _processStartTime = Process.GetCurrentProcess().StartTime.ToUniversalTime();

        private static bool _pauseAll;

        static GTween()
        {
            DefaultDispatchEvents = false;
            DefaultEase = LinearEase;
            PauseAll = false;
            Time = GetTimer() / 1000.0D;
            TimeScaleAll = 1.0;

            _frameThread = new Thread(AnimationFrameAction);
            _frameThread.Name = "frameRefresh";
            _frameThread.IsBackground = true;
            _frameThread.Start();
        }

        /// <summary>
        /// Constructs a new GTween instance.
        /// </summary>
        /// <param name="target">The object whose properties will be tweened.Defaults to null.</param>
        /// <param name="duration">The length of the tween in frames or seconds depending on the timingMode.Defaults to 1.</param>
        /// <param name="values">An object containing end property values.For example, to tween to x = 100, y= 100, you could pass { x: 100, y: 100} as the values object.</param>
        public GTween(object target = null, double duration = 1.0, IDictionary<string, double> values = null)
        {
            DispatchEvents = DefaultDispatchEvents;
            Ease = DefaultEase;
            Target = target;
            Duration = duration;

            AutoPlay = true;
            _paused = true;
            RepeatCount = 1;
            TimeScale = 1;

            ResetValues(values);

            if (Duration == 0 && Delay == 0 && AutoPlay)
            {
                Position = 0;
            }
        }

        /// <summary>
        /// Stops all tweens by cancelling the token used by the
        /// frame animation thread.
        /// </summary>
        public static void Destroy()
        {
            _cancelAnimation.Cancel();
            _playAnimation.Set();
        }

        /// <summary>
        /// Indicates whether the tween should automatically play when an end value is changed.
        /// </summary>
        public bool AutoPlay
        {
            get;
            set;
        }

        /// <summary>
        /// The current calculated position of the tween.
        /// This is a deterministic value between 0 and duration calculated
        /// from the current position based on the duration, repeatCount, and reflect properties.
        /// This is always a value between 0 and duration, whereas <code>.position</code> can range
        /// between -delay and repeatCount* duration.This should not be set directly.
        /// </summary>
        public double CalculatedPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// The previous calculated position of the tween. See <code>.calculatedPosition</code> for more information.
        /// This should not be set directly.
        /// </summary>
        public double CalculatedPositionOld
        {
            get;
            private set;
        }

        /// <summary>
        /// Allows you to associate arbitrary data with your tween. For example, you might use this to reference specific data when handling event callbacks from tweens.
        /// </summary>
        public object Data
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the default value of dispatchEvents for new instances.
        /// </summary>
        public static bool DefaultDispatchEvents
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the default easing function to use with new tweens. Set to GTween.linearEase by default.
        /// </summary>
        public static Easer DefaultEase
        {
            get;
            set;
        }

        /// <summary>
        /// The length of the delay in frames or seconds (depending on <code>.useFrames</code>).
        /// The delay occurs before a tween reads initial values or starts playing.
        /// </summary>
        public double Delay
        {
            get
            {
                return _delay;
            }

            set
            {
                if (_position <= 0)
                {
                    _position = -value;
                }
                _delay = value;
            }
        }

        /// <summary>
        /// If true, it will dispatch init, change, and complete events in addition to calling the
        /// onInit, onChange, and onComplete callbacks. Callbacks provide significantly better
        /// performance, whereas events are more standardized and flexible(allowing multiple
        /// listeners, for example).
        /// <br/><br/>
        /// By default this will use the value of defaultDispatchEvents.
        /// </summary>
        public bool DispatchEvents
        {
            get;
            set;
        }

        /// <summary>
        /// The length of the tween in frames or seconds (depending on the timingMode). Setting this will also update any child transitions that have synchDuration set to true.
        /// </summary>
        public double Duration
        {
            get;
            set;
        }

        /// <summary>
        /// The easing function to use for calculating the tween. This can be any standard tween function, such as the tween functions in fl.motion.easing.* that come with Flash CS3.
        /// New tweens will have this set to<code>defaultTween</code>.Setting this to null will cause GTween to throw null reference errors.
        /// </summary>
        public Easer Ease
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies another GTween instance that will have <code>paused=false</code> set on it when this tween completes.
        /// This happens immediately before onComplete is called.
        /// </summary>
        public GTween NextTween
        {
            get;
            set;
        }

        /// <summary>
        /// Setting this to true pauses all tween instances. This does not affect individual tweens' .paused property.
        /// </summary>
        public static bool PauseAll
        {
            get
            {
                return _pauseAll;
            }

            set
            {
                if (value == _pauseAll)
                {
                    return;
                }
                _pauseAll = value;

                if (_pauseAll)
                {
                    _playAnimation.Reset();
                }
                else
                {
                    _playAnimation.Set();
                }
            }
        }

        /// <summary>
        /// Plays or pauses a tween. You can still change the position value externally on a paused
        /// tween, but it will not be updated automatically.While paused is false, the tween is also prevented
        /// from being garbage collected while it is active.
        /// This is achieved in one of two ways:
        /// <list type="bulleted">
        /// <item>
        /// <description>If the target object is an IEventDispatcher, then the tween will subscribe to a dummy event using a hard reference.This allows
        /// the tween to be garbage collected if its target is also collected, and there are no other external references to it.</description>
        /// </item>
        /// <item>
        /// <description>If the target object is not an IEventDispatcher, then the tween is placed in a global list, to prevent collection until it is paused or completes.
        /// Note that pausing all tweens via the GTween.pauseAll static property will not free the tweens for collection.</description>
        /// </item>
        /// </list>
        /// </summary>
        public bool Paused
        {
            get
            {
                return _paused;
            }

            set
            {
                if (value == _paused)
                {
                    return;
                }
                _paused = value;

                if (!_paused)
                {
                    if (double.IsNaN(Position) || (RepeatCount != 0 && Position >= RepeatCount * Duration))
                    {
                        _initialized = false;
                        CalculatedPosition = CalculatedPositionOld = Ratio = RatioOld = PositionOld = 0;
                        _position = -Delay;
                    }

                    lock (_instancesPadlock)
                    {
                        _instances.Add(this);
                    }
                }
            }
        }

        /// <summary>
        /// Gets and sets the position of the tween in frames or seconds (depending on <code>.useFrames</code>). This value will
        /// be constrained between -delay and repeatCount* duration.It will be resolved to a<code> calculatedPosition</code> before
        /// being applied.
        ///
        /// <b>Negative values</b><br/>
        /// Values below 0 will always resolve to a calculatedPosition of 0. Negative values can be used to set up a delay on the tween, as the tween will have to count up to 0 before initing.
        ///
        /// <b>Positive values</b><br/>
        /// Positive values are resolved based on the duration, repeatCount, and reflect properties.
        /// </summary>
        public double Position
        {
            get
            {
                return _position;
            }

            set
            {
                PositionOld = Position;
                RatioOld = Ratio;
                CalculatedPositionOld = CalculatedPosition;

                var maxPosition = RepeatCount * Duration;
                var end = (value >= maxPosition && RepeatCount > 0);

                if (end)
                {
                    if (CalculatedPositionOld == maxPosition)
                    {
                        return;
                    }

                    _position = maxPosition;
                    var repeatOddTimes = (RepeatCount & 1) == 1;
                    CalculatedPosition = (Reflect && !(repeatOddTimes)) ? 0 : Duration;
                }
                else
                {
                    _position = value;
                    CalculatedPosition = _position < 0 ? 0 : _position % Duration;
                    var factor = _position / Duration;
                    var isOdd = ((int)factor & 1) == 1;
                    if (Reflect && isOdd)
                    {
                        CalculatedPosition = Duration - CalculatedPosition;
                    }
                }

                Ratio = (Duration == 0 && _position >= 0) ? 1.0 : Ease(CalculatedPosition / Duration, 0, 1, 1);
                if ((Target != null) && (_position >= 0 || PositionOld >= 0) && CalculatedPosition != CalculatedPositionOld)
                {
                    if (!_initialized)
                    {
                        Initialize();
                    }

                    foreach (var item in _values)
                    {
                        var initVal = _initValues[item.Key];
                        var rangeVal = _rangeValues[item.Key];
                        var val = initVal + rangeVal * Ratio;

                        Target.SetValue(item.Key, val);
                    }
                }

                if (!SuppressEvents)
                {
                    if (DispatchEvents)
                    {
                        OnChanged();
                    }
                }

                if (end)
                {
                    Paused = true;
                    if (NextTween != null)
                    {
                        NextTween.Paused = false;
                    }

                    if (!SuppressEvents)
                    {
                        if (DispatchEvents)
                        {
                            OnCompleted();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The position of the tween at the previous change. This should not be set directly.
        /// </summary>
        public double PositionOld
        {
            get;
            private set;
        }

        /// <summary>
        /// The eased ratio (generally between 0-1) of the tween at the current position. This should not be set directly.
        /// </summary>
        public double Ratio
        {
            get;
            private set;
        }

        /// <summary>
        /// The eased ratio (generally between 0-1) of the tween at the previous position. This should not be set directly.
        /// </summary>
        public double RatioOld
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates whether the tween should use the reflect mode when repeating. If reflect is set to true, then the tween will play backwards on every other repeat.
        /// </summary>
        public bool Reflect
        {
            get;
            set;
        }

        /// <summary>
        /// The number of times this tween will run. If 1, the tween will only run once. If 2 or more, the tween will repeat that many times. If 0, the tween will repeat forever.
        /// </summary>
        public int RepeatCount
        {
            get;
            set;
        }

        /// <summary>
        /// If true, events/callbacks will not be called. As well as allowing for more
        /// control over events, and providing flexibility for extension, this results
        /// in a slight performance increase, particularly if useCallbacks is false.
        /// </summary>
        public bool SuppressEvents
        {
            get;
            set;
        }

        /// <summary>
        /// The target object to tween. This can be any kind of object. You can retarget a tween at any time, but changing the target in mid-tween may result in unusual behaviour.
        /// </summary>
        public object Target
        {
            get;
            set;
        }

        protected static double Time
        {
            get;
            private set;
        }

        /// <summary>
        /// Allows you to scale the passage of time for a tween. For example, a tween with a duration of 5 seconds, and a timeScale of 2 will complete in 2.5 seconds.
        /// With a timeScale of 0.5 the same tween would complete in 10 seconds.
        /// </summary>
        public double TimeScale
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the time scale for all tweens. For example to run all tweens at half speed,
        /// you can set timeScaleAll to 0.5. It is multiplied against each tweens timeScale.
        /// For example a tween with timeScale=2 will play back at normal speed if timeScaleAll is set to 0.5.
        /// </summary>
        public static double TimeScaleAll
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the version number for this build.The numeric value will always
        /// increase with the version number for easy comparison(ex.GTween.version >= 2.12).
        /// Currently, it incorporates the major version as the integer value, and the two digit
        /// build number as the decimal value.For example, the fourth build of version 3 would
        /// have version= 3.04.
        /// </summary>
        public static Version Version
        {
            get
            {
                return typeof(GTween).Assembly.GetName().Version;
            }
        }

        /// <summary>
        /// Jumps the tween to its beginning and pauses it. This is the same as setting <code>position=0</code> and <code>paused=true</code>.
        /// </summary>
        public void Beginning()
        {
            Position = 0;
            Paused = true;
        }

        /// <summary>
        /// Removes a end value from the tween. This prevents the GTween instance from tweening the property.
        /// </summary>
        /// <param name="name">The name of the end property to delete.</param>
        /// <returns></returns>
        public bool DeleteValue(string name)
        {
            _rangeValues.Remove(name);
            _initValues.Remove(name);

            return _values.Remove(name);
        }

        /// <summary>
        /// Jumps the tween to its end and pauses it. This is roughly the same as setting <code>position=repeatCount*duration</code>.
        /// </summary>
        public void End()
        {
            Position = (RepeatCount > 0) ? RepeatCount * Duration : Duration;
        }

        /// <summary>
        /// Returns the initial value for the specified property.
        /// Note that the value will not be available until the tween inits.
        /// </summary>
        /// <param name="name">The name of the end property to retrieve.</param>
        /// <returns></returns>
        public double GetInitValue(string name)
        {
            return _initValues[name];
        }

        /// <summary>
        /// Returns the end value for the specified property if one exists.
        /// </summary>
        /// <param name="name">The name of the property to return a end value for.</param>
        /// <returns></returns>
        public double GetValue(string name)
        {
            return _values[name];
        }

        /// <summary>
        /// Returns the hash table of all end properties and their values. This is a copy of the internal hash of values, so modifying
        /// the returned object will not affect the tween.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, double> GetValues()
        {
            return Copy(_values, new Dictionary<string, double>());
        }

        /// <summary>
        /// Reads all of the initial values from target and calls the onInit callback.
        /// This is called automatically when a tween becomes active(finishes delaying)
        /// and when<code>.swapValues()</code> is called.It would rarely be used directly
        /// but is exposed for possible use by plugin developers or power users.
        /// </summary>
        public void Initialize()
        {
            _initialized = true;
            _initValues = new Dictionary<string, double>();
            _rangeValues = new Dictionary<string, double>();

            foreach (var item in _values)
            {
                _rangeValues[item.Key] = _values[item.Key] - (_initValues[item.Key] = Target.GetValue<double>(item.Key));
            }

            if (!SuppressEvents)
            {
                if (DispatchEvents)
                {
                    OnInitialized();
                }
            }
        }

        /// <summary>
        /// The default easing function used by GTween.
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="unused1"></param>
        /// <param name="unused2"></param>
        /// <param name="unused3"></param>
        /// <returns></returns>
        public static double LinearEase(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio;
        }

        /// <summary>
        /// Similar to <code>.setValues()</code>, but clears all previous end values
        /// before setting the new ones.
        /// </summary>
        /// <param name="values">An object containing end property values.</param>
        public void ResetValues(IDictionary<string, double> values = null)
        {
            _values = new Dictionary<string, double>();
            SetValues(values);
        }

        /// <summary>
        /// Shorthand method for making multiple setProperty calls quickly.
        /// This adds the specified properties to the values list.Passing a
        /// property with a value of null will delete that value from the list.
        ///
        /// <b>Example:</b> set x and y end values, delete rotation:<br/>
        /// <code>myGTween.setProperties({x:200, y:400, rotation:null});</code>
        ///
        /// </summary>
        /// <param name="values">An object containing end property values.</param>
        public void SetValues(IDictionary<string, double> values)
        {
            Copy(values, _values, true);
            Invalidate();
        }

        /// <summary>
        /// Swaps the init and end values for the tween, effectively reversing it.
        /// This should generally only be called before the tween starts playing.
        ///
        /// This will force the tween to init if it hasn't already done so, which
        /// may result in an onInit call.
        ///
        /// It will also force a render(so the target immediately jumps to the new values
        /// immediately) which will result in the onChange callback being called.
        ///
        /// You can also use the special "swapValues" property on the props parameter of
        /// the GTween constructor to call swapValues() after the values are set.
        ///
        /// The following example would tween the target from 100,100 to its current position:<br/>
        /// <code>new GTween(ball, 2, { x: 100, y: 100}, {swapValues:true});</code>
        /// </summary>
        public void SwapValues()
        {
            if (!_initialized)
            {
                Initialize();
            }
            var o = _values;
            _values = _initValues;
            _initValues = o;
            foreach (var item in _rangeValues)
            {
                _rangeValues[item.Key] *= -1;
            }

            if (_position < 0)
            {
                // render it at position 0:
                var pos = PositionOld;
                Position = 0;
                _position = PositionOld;
                PositionOld = pos;
            }
            else
            {
                Position = _position;
            }
        }

        /// <summary>
        /// Copies the key-value pairs from the first object to the second.
        /// </summary>
        /// <param name="source">The source object to copy the properties from.</param>
        /// <param name="destination">The destination object to copy the properties to.</param>
        /// <param name="smart">Reserved for future use.</param>
        /// <returns></returns>
        private IDictionary<string, double> Copy(IDictionary<string, double> source, IDictionary<string, double> destination, bool smart = false)
        {
            if (null == source || null == source)
            {
                return destination;
            }

            foreach (var item in source)
            {
                destination[item.Key] = source[item.Key];
            }

            return destination;
        }

        /// <summary>
        /// Method that executes on the frame thread to update the
        /// position of all tween instances on the timeline. This
        /// mechanism replaces the frame-based timeline that is
        /// provided by the Flash Player in ActionScript.
        /// </summary>
        private static void AnimationFrameAction()
        {
            var mre = new ManualResetEvent(false);

            do
            {
                if (PauseAll)
                {
                    _playAnimation.WaitOne();
                }

                var t = Time;
                Time = GetTimer() / 1000.0D;

                var dt = (Time - t) * TimeScaleAll;
                lock (_instancesPadlock)
                {
                    foreach (var tween in _instances)
                    {
                        tween.Position = tween._position + dt * tween.TimeScale;
                    }
                }

                _instances.RemoveWhere(i =>
                {
                    return i._paused;
                });

                // Pause the thread for 1/16 of a second to achieve 60 fps
                mre.WaitOne(16);
            } while (!_cancelAnimation.Token.IsCancellationRequested);
        }

        /// <summary>
        /// Returns the number of milliseconds elapsed since the process was started.
        /// </summary>
        /// <returns></returns>
        private static double GetTimer()
        {
            return (DateTime.UtcNow - _processStartTime).TotalMilliseconds;
        }

        /// <summary>
        /// Resets the tween back to its original position.
        /// </summary>
        private void Invalidate()
        {
            _initialized = false;
            if (_position > 0)
            {
                _position = 0;
            }

            if (AutoPlay)
            {
                Paused = false;
            }
        }

        #region EventDispatchers

        private void OnChanged()
        {
            var args = new GTweenEventArgs(this);
            var handlers = Volatile.Read(ref Changed)?.GetInvocationList();
            foreach (EventHandler<GTweenEventArgs> handler in handlers ?? Enumerable.Empty<Delegate>())
            {
                handler.BeginInvoke(this, args, null, null);
            }
        }

        private void OnCompleted()
        {
            var args = new GTweenEventArgs(this);
            var handlers = Volatile.Read(ref Completed)?.GetInvocationList();
            foreach (EventHandler<GTweenEventArgs> handler in handlers ?? Enumerable.Empty<Delegate>())
            {
                handler.BeginInvoke(this, args, null, null);
            }
        }

        private void OnInitialized()
        {
            var args = new GTweenEventArgs(this);
            var handlers = Volatile.Read(ref Initialized)?.GetInvocationList();
            foreach (EventHandler<GTweenEventArgs> handler in handlers ?? Enumerable.Empty<Delegate>())
            {
                handler.BeginInvoke(this, args, null, null);
            }
        }

        #endregion EventDispatchers
    }
}