using MongoDB.Dev.Core.Descriptors;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Abstracts;

public abstract class ModelOverviewProviderAbstract : IModelOverviewProvider
{
    public abstract ModelOverview GetModelOverview<TReadModel>() where TReadModel : IMongoModel;
}