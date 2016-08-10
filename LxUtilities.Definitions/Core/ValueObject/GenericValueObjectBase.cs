namespace LxUtilities.Definitions.Core.ValueObject
{
    public abstract class GenericValueObjectBase<T> : IValueObject<T>
    {
        protected GenericValueObjectBase()
        {
        }

        protected GenericValueObjectBase(T value)
        {
            Value = value;
        }

        public T Value { get; protected set; }

        public virtual void SetValue(T value)
        {
            Value = value;
        }
    }
}