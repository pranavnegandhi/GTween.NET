using System;

namespace GSkinner.Motion.Easing
{
    public class Circular
    {
        // unused params are included for compatibility with other easing classes.
        public static double easeIn(double ratio, double unused1, double unused2, double unused3)
        {
            return -(Math.Sqrt(1 - ratio * ratio) - 1);
        }

        public static double easeOut(double ratio, double unused1, double unused2, double unused3)
        {
            return Math.Sqrt(1 - (ratio - 1) * (ratio - 1));
        }

        public static double easeInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return ((ratio *= 2) < 1) ? -0.5 * (Math.Sqrt(1 - ratio * ratio) - 1) : 0.5 * (Math.Sqrt(1 - (ratio -= 2) * ratio) + 1);
        }
    }
}