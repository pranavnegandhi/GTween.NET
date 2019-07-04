using static GSkinner.Motion.GTween;

namespace GSkinner.Motion
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

        public double Position
        {
            get;
            set;
        } = 0;

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

        public bool SuppressEvents
        {
            get;
            set;
        } = true;

        public double TimeScale
        {
            get;
            set;
        } = 1;
    }
}