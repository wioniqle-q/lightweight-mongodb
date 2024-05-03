namespace MongoDB.Dev.Core.Utilities.Common;

public abstract class SharedEquatable<T> : IEquatable<T> where T : SharedEquatable<T>
{
    private static readonly IEqualityComparer<object> Comparer = EqualityComparer<object>.Default;

    public bool Equals(T? other)
    {
        if (ReferenceEquals(this, other))
            return true;

        return other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents(), Comparer!);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as T);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return GetEqualityComponents().Select(component => component?.GetHashCode() ?? 0)
                .Aggregate(17, (current, componentHash) => (current ^ componentHash) * 31);
        }
    }

    public override string ToString()
    {
        return string.Join(", ", GetType().Name,
            string.Join(", ", GetEqualityComponents().Select(obj => obj?.ToString() ?? "null")));
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();
}
