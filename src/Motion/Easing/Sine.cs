using System;

namespace GSkinner.Motion.Easing
{
    public class Sine
    {
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return 1 - Math.Cos(ratio * (Math.PI / 2));
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return Math.Sin(ratio * (Math.PI / 2));
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return -0.5 * (Math.Cos(ratio * Math.PI) - 1);
        }
    }
}