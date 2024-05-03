using MongoDB.Dev.Core.ValueObjects;

namespace MongoDB.Dev.Core.Descriptors;

public sealed class ModelOverview : ValueObject<string>
{
    private ModelOverview(string parameter) : base(parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException("ModelOverview collection name cannot be null or empty", nameof(parameter));
    }

    public static implicit operator ModelOverview(string parameter)
    {
        return new ModelOverview(parameter);
    }

    public static implicit operator string(ModelOverview overview)
    {
        return overview.Value;
    }

    public static ModelOverview FromType(Type modelType)
    {
        return new ModelOverview(modelType.Name);
    }
}