namespace MongoDB.Dev.Core.ValueObjects;

public abstract class ScalarValueObject<T>(T parameter) : IScalarValueObject<T>
{
    public T Value { get; } = parameter;

    public virtual bool Equals(IScalarValueObject<T>? other)
    {
        return other is not null &&
               (ReferenceEquals(this, other) ||
                EqualityComparer<T>.Default.Equals(Value, other.Value));
    }

    public override bool Equals(object? obj)
    {
        return obj is IScalarValueObject<T> other && Equals(other);
    }

    public static bool operator ==(ScalarValueObject<T> left, ScalarValueObject<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ScalarValueObject<T> left, ScalarValueObject<T> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }
}