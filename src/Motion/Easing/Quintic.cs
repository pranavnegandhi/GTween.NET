namespace GSkinner.Motion.Easing
{
    public static class Quintic
    {
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio * ratio * ratio * ratio * ratio;
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            return 1 + (ratio -= 1) * ratio * ratio * ratio * ratio;
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            return (ratio < 0.5) ? 16 * ratio * ratio * ratio * ratio * ratio : 16 * (ratio -= 1) * ratio * ratio * ratio * ratio + 1;
        }
    }
}