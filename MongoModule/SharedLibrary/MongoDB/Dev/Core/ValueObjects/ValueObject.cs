namespace MongoDB.Dev.Core.ValueObjects;

public abstract class ValueObject<T>(T parameter) : IEquatable<ValueObject<T>>
{
    protected T Value { get; } = parameter;

    public virtual bool Equals(ValueObject<T>? other)
    {
        return other is not null &&
               (ReferenceEquals(this, other) ||
                EqualityComparer<T>.Default.Equals(Value, other.Value));
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject<T> other && Equals(other);
    }

    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }
}