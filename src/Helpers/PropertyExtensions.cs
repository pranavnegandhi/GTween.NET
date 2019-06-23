using System.Reflection;

namespace GSkinner.Helpers
{
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