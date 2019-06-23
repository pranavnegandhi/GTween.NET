namespace GSkinner.Motion.Easing
{
    public class Back
    {
        private const double S = 1.70158;

        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio * ratio * ((S + 1) * ratio - S);
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio -= 1) * ratio * ((S + 1) * ratio + S) + 1;
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return ((ratio *= 2) < 1) ? 0.5 * (ratio * ratio * ((S * 1.525 + 1) * ratio - S * 1.525)) : 0.5 * ((ratio -= 2) * ratio * ((S * 1.525 + 1) * ratio + S * 1.525) + 2);
        }
    }
}