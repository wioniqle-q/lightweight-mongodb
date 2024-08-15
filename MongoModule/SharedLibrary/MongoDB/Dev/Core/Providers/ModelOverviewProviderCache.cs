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
        return _cache.TryGetValue(readModelType, out var modelOverview)
            ? modelOverview
            : _cache.GetValue(readModelType, key => valueFactory(key));
    }
}