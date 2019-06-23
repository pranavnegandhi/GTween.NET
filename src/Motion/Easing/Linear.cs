namespace GSkinner.Motion.Easing
{
    public class Linear
    {
        // unused params are included for compatibility with other easing classes.
        public static double EaseNone(double ratio, double unused1, double unused2, double unused3)
        {
            return ratio;
        }
    }
}