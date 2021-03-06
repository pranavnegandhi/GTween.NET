﻿namespace GSkinner.Motion
{
    public class GTweenPropertyInitializer
    {
        public bool AutoPlay
        {
            get;
            set;
        } = true;

        public object Data
        {
            get;
            set;
        }

        public double Delay
        {
            get;
            set;
        } = 0;

        public bool DispatchEvents
        {
            get;
            set;
        } = false;

        public bool Paused
        {
            get;
            set;
        } = true;

        public double? Position
        {
            get;
            set;
        } = null;

        public bool Reflect
        {
            get;
            set;
        } = false;

        public int RepeatCount
        {
            get;
            set;
        } = 1;

        public double TimeScale
        {
            get;
            set;
        } = 1;
    }
}