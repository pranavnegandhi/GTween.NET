using System;

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
    public class Elastic
    {
        private const double a = 1;

        private const double p = 0.3;

        private const double s = p / 4;

        // unused params are included for compatibility with other easing classes.
        public static double EaseIn(double ratio, double unused1, double unused2, double unused3)
        {
            if (ratio == 0 || ratio == 1)
            {
                return ratio;
            }

            return -(a * Math.Pow(2, 10 * (ratio -= 1)) * Math.Sin((ratio - s) * (2 * Math.PI) / p));
        }

        public static double EaseOut(double ratio, double unused1, double unused2, double unused3)
        {
            if (ratio == 0 || ratio == 1)
            {
                return ratio;
            }

            return a * Math.Pow(2, -10 * ratio) * Math.Sin((ratio - s) * (2 * Math.PI) / p) + 1;
        }

        public static double EaseInOut(double ratio, double unused1, double unused2, double unused3)
        {
            if (ratio == 0 || ratio == 1)
            {
                return ratio;
            }

            ratio = ratio * 2 - 1;

            if (ratio < 0)
            {
                return -0.5 * (a * Math.Pow(2, 10 * ratio) * Math.Sin((ratio - s * 1.5) * (2 * Math.PI) / (p * 1.5)));
            }

            return 0.5 * a * Math.Pow(2, -10 * ratio) * Math.Sin((ratio - s * 1.5) * (2 * Math.PI) / (p * 1.5)) + 1;
        }
    }
}