using System;

namespace GSkinner.Motion.Easing
{
    public class Exponential
    {
        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio == 0) ? 0 : Math.Pow(2, 10 * (ratio - 1));
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio == 1) ? 1 : 1 - Math.Pow(2, -10 * ratio);
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            if (ratio == 0 || ratio == 1)
            {
                return ratio;
            }

            if (0 > (ratio = ratio * 2 - 1))
            {
                return 0.5 * Math.Pow(2, 10 * ratio);
            }

            return 1 - 0.5 * Math.Pow(2, -10 * ratio);
        }
    }
}