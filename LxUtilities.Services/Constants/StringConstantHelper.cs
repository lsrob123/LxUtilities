using System;
using System.Linq;
using System.Reflection;

namespace LxUtilities.Services.Constants
{
    public static class StringConstantHelper
    {
        public static string GetValue<T>(string input, string defaultValue = null)
        {
            return GetValue(typeof(T), input, defaultValue);
        }

        public static string GetValue(Type subClassType, string input, string defaultValue)
        {
            var match = subClassType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(x => x.IsLiteral &&
                                     !x.IsInitOnly &&
                                     input.Equals((string) x.GetRawConstantValue(), StringComparison.OrdinalIgnoreCase));

            if (match != null)
                return (string) match.GetRawConstantValue();

            return defaultValue;
        }
    }
}