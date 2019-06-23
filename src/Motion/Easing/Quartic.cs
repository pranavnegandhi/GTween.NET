namespace GSkinner.Motion.Easing
{
    public class Quartic
    {
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio * ratio * ratio * ratio;
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return 1 - (ratio -= 1) * ratio * ratio * ratio;
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio < 0.5) ? 8 * ratio * ratio * ratio * ratio : -8 * (ratio -= 1) * ratio * ratio * ratio + 1;
        }
    }
}