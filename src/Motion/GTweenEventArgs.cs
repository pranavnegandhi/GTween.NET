using System;

namespace GSkinner.Motion
{
    public class GTweenEventArgs : EventArgs
    {
        public GTweenEventArgs(GTween tween)
        {
            Tween = tween;
        }

        public GTween Tween
        {
            get;
            private set;
        }
    }
}