using System.Reflection;

namespace GSkinner.Helpers
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
    public static class PropertyExtensions
    {
        public static void SetValue<TValue>(this object target, string name, TValue value)
        {
            var property = target.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (null != property && property.CanWrite && property.PropertyType == value.GetType())
            {
                property.SetValue(target, value, null);
            }
        }

        public static TValue GetValue<TValue>(this object target, string name)
        {
            var property = target.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (null != property && property.CanRead && property.PropertyType == default(TValue).GetType())
            {
                return (TValue)property.GetValue(target);
            }

            return default(TValue);
        }
    }
}