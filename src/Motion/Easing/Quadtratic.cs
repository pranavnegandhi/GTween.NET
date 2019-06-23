namespace GSkinner.Motion.Easing
{
    public class Quadratic
    {
        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio * ratio;
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return -ratio * (ratio - 2);
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio < 0.5) ? 2 * ratio * ratio : -2 * ratio * (ratio - 2) - 1;
        }
    }
}