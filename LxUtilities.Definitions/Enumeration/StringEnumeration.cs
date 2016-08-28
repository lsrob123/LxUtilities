using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LxUtilities.Definitions.Enumeration
{
    public abstract class StringEnumeration : IComparable
    {
        protected StringEnumeration()
        {
        }

        protected StringEnumeration(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public int CompareTo(object other)
        {
            return string.Compare(Value, ((StringEnumeration) other).Value, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return Value;
        }

        public static IEnumerable<T> GetAll<T>() where T : StringEnumeration, new()
        {
            var fields =
                typeof (T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).ToList();

            return (from info in fields let instance = new T() select info.GetValue(instance)).OfType<T>();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as StringEnumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static T FromValue<T>(string value) where T : StringEnumeration, new()
        {
            var matchingItem = Parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : StringEnumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem != null)
                return matchingItem;

            var message = $"'{value}' is not a valid {description} in {typeof (T)}";
            throw new ApplicationException(message);
        }
    }
}