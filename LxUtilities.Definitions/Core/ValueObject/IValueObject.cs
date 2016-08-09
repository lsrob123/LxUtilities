namespace LxUtilities.Definitions.Core.ValueObject
{
    public interface IValueObject
    {
    }

    public interface IValueObject<T>
    {
        T Value { get; }

        void SetValue(T value);
    }
}