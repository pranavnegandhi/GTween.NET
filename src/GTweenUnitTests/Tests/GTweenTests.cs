using GSkinner.Motion;
using GSkinner.Tests.Targets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GSkinner.Tests
{
    [TestClass]
    public class GTweenTests
    {
        private const double TweenDuration = 5;

        private const int WaitDuration = 10 * 1000;

        private Point1D _target;

        private IDictionary<string, double> _values;

        [TestInitialize]
        public void InitializeTest()
        {
            _target = new Point1D() { Value = 0 };
            _values = new Dictionary<string, double>() { { "Value", 10d } };
        }

        [TestMethod]
        public void TestAutoPlayOn()
        {
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = true,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                reset.Set();
            };
            tween.Completed += handler;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.AreEqual(true, tween.AutoPlay);
            Assert.IsTrue(tween.Position >= TweenDuration);
            Assert.AreEqual(_values["Value"], _target.Value);
        }

        [TestMethod]
        public void TestAutoPlayOff()
        {
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                reset.Set();
            };
            tween.Changed += handler;
            reset.WaitOne(WaitDuration);
            tween.Changed -= handler;

            Assert.AreEqual(false, tween.AutoPlay);
            Assert.AreEqual(0d, tween.Position);
            Assert.AreEqual(0d, _target.Value);
        }

        [TestMethod]
        public void TestBeginning()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.Beginning();

            Assert.AreEqual(true, tween.Paused);
            Assert.AreEqual(0, tween.Position);
        }

        [TestMethod]
        public void TestChanged()
        {
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = true,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Changed += handler;
            reset.WaitOne(WaitDuration);
            tween.Changed -= handler;

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void TestCompleted()
        {
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = true,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Completed += handler;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void TestDataAtInitialization()
        {
            var data = $"Store for {nameof(GTweenTests)}";
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = true,
                Data = data,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                reset.Set();
            };
            tween.Completed += handler;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.AreEqual(data, tween.Data);
        }

        [TestMethod]
        public void TestDataAfterInstantiation()
        {
            var initData = $"Store for {nameof(GTweenTests)} at initialization";
            var data = $"Store for {nameof(GTweenTests)} after instantiation";

            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = true,
                Data = initData,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);
            tween.Data = data;

            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                reset.Set();
            };
            tween.Completed += handler;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.AreEqual(data, tween.Data);
        }

        /// <summary>
        /// The value of DefaultDispatchEvents is set to false by default.
        /// </summary>
        [TestMethod]
        public void TestDefaultDispatchEventsDisabled()
        {
            var tween = new GTween(_target, TweenDuration, _values);

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsFalse(eventFired);
        }

        /// <summary>
        /// Explicitly change the value of DefaultDispatchEvents to true.
        /// </summary>
        [TestMethod]
        public void TestDefaultDispatchEventsEnabled()
        {
            GTween.DefaultDispatchEvents = true;
            var tween = new GTween(_target, TweenDuration, _values);

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void TestDelay()
        {
            var initializer = new GTweenPropertyInitializer();
            initializer.AutoPlay = false;
            initializer.DispatchEvents = true;
            initializer.SuppressEvents = false;
            initializer.Delay = TweenDuration;

            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var reset = new AutoResetEvent(false);
            var timer = new Stopwatch();
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                timer.Stop();
                reset.Set();
            };
            tween.Initialized += handler;
            timer.Start();
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Initialized -= handler;

            Assert.IsTrue(timer.ElapsedMilliseconds >= TweenDuration);
        }

        [TestMethod]
        public void TestDeleteValue()
        {
            var initializer = new GTweenPropertyInitializer()
            {
                AutoPlay = false,
                DispatchEvents = true,
                SuppressEvents = false
            };
            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                tween.DeleteValue("Value");
                reset.Set();
            };
            tween.Changed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Changed -= handler;

            Assert.AreEqual(false, tween.Paused);
            Assert.IsTrue(_target.Value < _values["Value"]);
            Assert.IsTrue(_target.Value > 0);
        }

        /// <summary>
        /// The value of DispatchEvents is set to false by default.
        /// </summary>
        [TestMethod]
        public void TestDispatchEventsDisabled()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.DispatchEvents = false;
            tween.SuppressEvents = true;

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsFalse(eventFired);
        }

        /// <summary>
        /// Explicitly change the value of DispatchEvents to true.
        /// </summary>
        [TestMethod]
        public void TestDispatchEventsEnabled()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.DispatchEvents = true;
            tween.SuppressEvents = false;

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void TestDuration()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.DispatchEvents = true;
            tween.SuppressEvents = false;

            var reset = new AutoResetEvent(false);
            var timer = new Stopwatch();
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                timer.Stop();
                reset.Set();
            };
            tween.Completed += handler;
            timer.Start();
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.IsTrue(timer.ElapsedMilliseconds >= TweenDuration * 1000);
        }

        [TestMethod]
        public void TestEnd()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.End();

            Assert.AreEqual(TweenDuration, tween.Position);
        }

        [TestMethod]
        public void TestGetInitValue()
        {
            var atStart = _target.Value;
            var tween = new GTween(_target, TweenDuration, _values);
            tween.DispatchEvents = true;
            tween.SuppressEvents = false;

            var afterInit = 0d;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                afterInit = tween.GetInitValue("Value");
                reset.Set();
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Completed -= handler;

            Assert.AreEqual(atStart, afterInit);
        }

        [TestMethod]
        public void TestGetValue()
        {
            var fromValues = _values["Value"];
            var tween = new GTween(_target, TweenDuration, _values);
            var fromInstance = tween.GetValue("Value");

            Assert.AreEqual(fromValues, fromInstance);
        }

        [TestMethod]
        public void TestGetValues()
        {
            KeyValuePair<string, double>[] fromValues = new KeyValuePair<string, double>[_values.Count];
            _values.CopyTo(fromValues, 0);

            var tween = new GTween(_target, TweenDuration, _values);
            var fromInstance = tween.GetValues();

            Assert.AreEqual(fromValues.Length, fromInstance.Count);
            Assert.IsTrue(fromInstance.ContainsKey(fromValues[0].Key));
        }

        [TestMethod]
        public void TestInitialized()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.DispatchEvents = true;
            tween.SuppressEvents = false;

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            tween.Initialized += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration);
            tween.Initialized -= handler;

            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void TestNextTween()
        {
            var initializer = new GTweenPropertyInitializer();
            initializer.AutoPlay = false;
            initializer.DispatchEvents = false;
            initializer.SuppressEvents = true;

            var secondValues = new Dictionary<string, double>() { { "Value", 100 } };

            var secondTween = new GTween(_target, TweenDuration, secondValues, initializer);
            secondTween.DispatchEvents = true;
            secondTween.SuppressEvents = false;

            initializer.DispatchEvents = false;
            initializer.SuppressEvents = true;

            var firstTween = new GTween(_target, TweenDuration, _values, initializer);
            firstTween.NextTween = secondTween;

            var eventFired = false;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                eventFired = true;
                reset.Set();
            };
            secondTween.Completed += handler;
            firstTween.Paused = false;
            reset.WaitOne(WaitDuration << 1);
            secondTween.Completed -= handler;

            Assert.IsTrue(eventFired);
            Assert.AreEqual(secondValues["Value"], _target.Value);
        }

        [TestMethod]
        public void TestPauseAll()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            GTween.PauseAll = true;
            var firstPosition = tween.Position = 1d;

            var reset = new AutoResetEvent(false);
            reset.WaitOne(WaitDuration);

            var secondPosition = tween.Position;

            Assert.AreEqual(firstPosition, secondPosition);
        }

        [TestMethod]
        public void TestPause()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.Paused = true;
            var firstPosition = tween.Position = 1d;

            var reset = new AutoResetEvent(false);
            reset.WaitOne(WaitDuration);

            var secondPosition = tween.Position;

            Assert.AreEqual(firstPosition, secondPosition);
        }

        [TestMethod]
        public void TestPosition()
        {
            var tween = new GTween(_target, TweenDuration, _values);
            tween.Paused = true;
            var position = 5d;
            tween.Position = 5d;

            Assert.AreEqual(position, tween.Position);
        }

        [TestMethod]
        public void TestReflect()
        {
            var initializer = new GTweenPropertyInitializer();
            initializer.AutoPlay = false;
            initializer.DispatchEvents = true;
            initializer.SuppressEvents = false;
            initializer.Reflect = true;
            initializer.RepeatCount = 2;

            var tween = new GTween(_target, TweenDuration, _values, initializer);
            var initValue = _target.Value;
            var midAchieved = false;
            var endAchieved = false;

            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                if (Math.Ceiling(_target.Value) == _values["Value"])
                {
                    midAchieved = true;
                }

                endAchieved = midAchieved && _target.Value == initValue;
            };

            tween.Changed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration << 1);
            tween.Changed -= handler;

            Assert.IsTrue(midAchieved);
            Assert.IsTrue(endAchieved);
        }

        [TestMethod]
        public void TestRepeat()
        {
            var initializer = new GTweenPropertyInitializer();
            initializer.AutoPlay = false;
            initializer.DispatchEvents = true;
            initializer.SuppressEvents = false;
            initializer.RepeatCount = 2;

            var tween = new GTween(_target, TweenDuration, _values, initializer);

            var counter = 0;
            var reset = new AutoResetEvent(false);
            EventHandler<GTweenEventArgs> handler = (s, e) =>
            {
                counter++;

                if (tween.RepeatCount == counter)
                {
                    reset.Set();
                }
            };
            tween.Completed += handler;
            tween.Paused = false;
            reset.WaitOne(WaitDuration << 1);
            tween.Completed -= handler;

            Assert.AreEqual(tween.RepeatCount, counter);
        }
    }
}