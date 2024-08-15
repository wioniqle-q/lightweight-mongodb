namespace MongoDB.Dev.Core.ValueObjects;

public interface IScalarValueObject<T> : IEquatable<IScalarValueObject<T>>
{
    public T Value { get; }
}