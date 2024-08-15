using MongoDB.Dev.Core.Abstracts;
using MongoDB.Dev.Core.Descriptors;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Providers;

internal sealed class ModelOverviewProvider : ModelOverviewProviderAbstract
{
    private static readonly IModelOverviewProviderCache Cache = ModelOverviewProviderCache.GetInstance();

    public override ModelOverview GetModelOverview<TReadModel>()
    {
        return Cache.GetOrAdd(typeof(TReadModel), CreateModelOverview);
    }

    private static ModelOverview CreateModelOverview(Type readModelType)
    {
        return ModelOverview.FromType(readModelType);
    }
}