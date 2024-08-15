using MongoDB.Dev.Core.Descriptors;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Abstracts;

internal abstract class ModelOverviewProviderCacheAbstract : IModelOverviewProviderCache
{
    public abstract ModelOverview GetOrAdd(Type readModelType, Func<Type, ModelOverview> valueFactory);
}