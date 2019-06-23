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