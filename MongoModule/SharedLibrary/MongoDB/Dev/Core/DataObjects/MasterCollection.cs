using MongoDB.Dev.Core.ValueObjects;

namespace MongoDB.Dev.Core.DataObjects;

public sealed class MasterCollection : ScalarValueObject<string>
{
    public MasterCollection(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Master collection name cannot be null or empty", nameof(value));
    }

    public static implicit operator MasterCollection(string value)
    {
        return new MasterCollection(value);
    }

    public static implicit operator string(MasterCollection name)
    {
        return name.Value;
    }
}