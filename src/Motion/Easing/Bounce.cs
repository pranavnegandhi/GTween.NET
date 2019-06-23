namespace GSkinner.Motion.Easing
{
    public class Bounce
    {
        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return 1 - EaseOut(1 - ratio, 0, 0, 0);
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            if (ratio < 1 / 2.75)
            {
                return 7.5625 * ratio * ratio;
            }
            else if (ratio < 2 / 2.75)
            {
                return 7.5625 * (ratio -= 1.5 / 2.75) * ratio + 0.75;
            }
            else if (ratio < 2.5 / 2.75)
            {
                return 7.5625 * (ratio -= 2.25 / 2.75) * ratio + 0.9375;
            }
            else
            {
                return 7.5625 * (ratio -= 2.625 / 2.75) * ratio + 0.984375;
            }
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return ((ratio *= 2) < 1) ? 0.5 * EaseIn(ratio, 0, 0, 0) : 0.5 * EaseOut(ratio - 1, 0, 0, 0) + 0.5;
        }
    }
}