namespace GSkinner.Motion.Easing
{
    public class Cubic
    {
        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio * ratio * ratio;
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio -= 1) * ratio * ratio + 1;
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio < 0.5) ? 4 * ratio * ratio * ratio : 4 * (ratio -= 1) * ratio * ratio + 1;
        }
    }
}