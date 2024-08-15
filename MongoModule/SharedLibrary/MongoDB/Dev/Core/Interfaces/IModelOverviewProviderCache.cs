using MongoDB.Dev.Core.Descriptors;

namespace MongoDB.Dev.Core.Interfaces;

internal interface IModelOverviewProviderCache
{
    public ModelOverview GetOrAdd(Type readModelType, Func<Type, ModelOverview> valueFactory);
}