using MongoDB.Dev.Core.Descriptors;

namespace MongoDB.Dev.Core.Interfaces;

public interface IModelOverviewProvider
{
    public ModelOverview GetModelOverview<TReadModel>() where TReadModel : IMongoModel;
}