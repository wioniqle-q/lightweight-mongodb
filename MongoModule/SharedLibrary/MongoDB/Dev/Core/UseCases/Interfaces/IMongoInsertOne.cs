using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoInsertOne<T> where T : IMongoModel
{
    public Task<T> InsertOneAsync(IMongoCollection<T> collection, T model, IClientSessionHandle session,
        CancellationToken cancellationToken = default);
}