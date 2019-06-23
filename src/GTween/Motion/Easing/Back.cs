namespace GSkinner.Motion.Easing
{
    /// <summary>
    /// <para>GTween.NET</para>
    /// <para>Based on GTween 2.01 for ActionScript 3 http://gskinner.com/libraries/gtween/</para>
    /// <para>This code is MIT licensed, see http://www.opensource.org/licenses/mit-license.php</para>
    /// <para>Copyright (c) 2019 Pranav Negandhi (http://www.notadesigner.com)</para>
    ///
    /// <para>GTween 2.01 for ActionScript 3 is MIT licensed, see http://www.opensource.org/licenses/mit-license.php</para>
    /// <para>Copyright(c) 2009 Grant Skinner</para>
    /// </summary>
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