using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoSyncFind<T> where T : IMongoModel
{
    public IMongoSyncFind<T> Where(Expression<Func<T, bool>> expression);

    public Task<T> SyncFind(IMongoCollection<T> collection, IClientSessionHandle session,
        CancellationToken cancellationToken = default);
}