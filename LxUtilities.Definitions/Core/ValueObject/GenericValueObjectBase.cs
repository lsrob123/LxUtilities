namespace LxUtilities.Definitions.Core.ValueObject
{
    public abstract class GenericValueObjectBase<T> : IValueObject
    {
        protected GenericValueObjectBase(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}