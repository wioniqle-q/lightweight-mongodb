using System.Runtime.CompilerServices;
using MongoDB.Dev.Core.Abstracts;
using MongoDB.Dev.Core.Descriptors;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Providers;

internal sealed class ModelOverviewProviderCache : ModelOverviewProviderCacheAbstract
{
    private static readonly Lazy<IModelOverviewProviderCache> Instance = new(() => new ModelOverviewProviderCache());
    private readonly ConditionalWeakTable<Type, ModelOverview> _cache = new();

    public static IModelOverviewProviderCache GetInstance()
    {
        return Instance.Value;
    }

    public override ModelOverview GetOrAdd(Type readModelType, Func<Type, ModelOverview> valueFactory)
    {
        if (_cache.TryGetValue(readModelType, out var modelOverview)) return modelOverview;

        modelOverview = CreateModelOverview(readModelType, valueFactory);
        _cache.Add(readModelType, modelOverview);
        return modelOverview;

        static ModelOverview CreateModelOverview(Type type, Func<Type, ModelOverview> valueFactory)
        {
            return valueFactory(type);
        }
    }
}
