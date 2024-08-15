using MongoDB.Dev.Core.ValueObjects;

namespace MongoDB.Dev.Core.DataObjects;

public sealed class SharedLockCollection : ScalarValueObject<int>
{
    private SharedLockCollection(int value) : base(value)
    {
        if (value < 1)
            throw new ArgumentException("SharedLockCollection count cannot be less than 1", nameof(value));
    }

    public static implicit operator SharedLockCollection(int value)
    {
        return new SharedLockCollection(value);
    }

    public static implicit operator int(SharedLockCollection options)
    {
        return options.Value;
    }
}