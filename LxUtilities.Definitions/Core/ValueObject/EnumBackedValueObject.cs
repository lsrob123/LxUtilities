using System;
using System.Globalization;

namespace LxUtilities.Definitions.Core.ValueObject
{
    public abstract class EnumBackedValueObject<TBackingEnum> : GenericValueObjectBase<string>
        where TBackingEnum : struct, IComparable, IConvertible, IFormattable
    {
        protected int MaxLength = 50;

        protected EnumBackedValueObject(int maxLength = 0)
        {
            SetMaxLength(maxLength);
        }

        protected EnumBackedValueObject(string value, int maxValueLength = 0) : this(maxValueLength)
        {
            SetStringValue(value);
        }

        protected EnumBackedValueObject(TBackingEnum value, int maxValueLength = 0) : this(maxValueLength)
        {
            SetValue(value);
        }

        public override void SetValue(string value)
        {
            SetStringValue(value);
        }

        private void SetStringValue(string value)
        {
            TBackingEnum enumValue;
            if (value == null ||
                (MaxLength > 0 && value.Length > MaxLength) ||
                !Enum.TryParse(value, true, out enumValue))
                Value = null;
            else
                SetValue(enumValue);
        }

        public void SetMaxLength(int maxLength)
        {
            MaxLength = maxLength;
        }

        public void SetValue(TBackingEnum enumValue)
        {
            Value = enumValue.ToString(CultureInfo.InvariantCulture);
        }

        public virtual bool Equals(TBackingEnum enumValue)
        {
            return Value.Equals(enumValue.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal);
        }
    }
}