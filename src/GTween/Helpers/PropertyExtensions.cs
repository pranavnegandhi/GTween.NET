using System;
using System.Linq.Expressions;
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
        public static TValue GetValue<TValue>(this object target, string name)
        {
            var property = target.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (null != property && property.CanRead && property.PropertyType == default(TValue).GetType())
            {
                return (TValue)property.GetValue(target);
            }

            return default(TValue);
        }

        public static Action<object, double> GenerateSetPropertyAction(this Type type, string property)
        {
            var pi = type.GetProperty(property, BindingFlags.Instance | BindingFlags.Public);
            var mi = pi.GetSetMethod();

            var targetParam = Expression.Parameter(typeof(object), "instance");
            var valueParam = Expression.Parameter(typeof(double), "value");

            var instance = Expression.Convert(targetParam, type);
            var value = Expression.Convert(valueParam, pi.PropertyType);
            var callExpr = Expression.Call(instance, mi, value);
            
            return Expression.Lambda<Action<object, double>>(callExpr, targetParam, valueParam).Compile();
        }
    }
}