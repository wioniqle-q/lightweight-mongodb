using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoInsertMany<T> where T : IMongoModel
{
    public Task<IEnumerable<T>> InsertManyAsync(IMongoCollection<T> collection, IEnumerable<T> models,
        IClientSessionHandle session, CancellationToken cancellationToken = default);
}