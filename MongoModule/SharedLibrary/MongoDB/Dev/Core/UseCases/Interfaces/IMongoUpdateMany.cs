using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoUpdateMany<T> where T : IMongoModel
{
    public IMongoUpdateMany<T> Where(Expression<Func<T, bool>> expression);
    public IMongoUpdateMany<T> Set(UpdateDefinition<T> updateDefinition);

    public Task<IAsyncCursor<T>> UpdateManyAsync(IMongoCollection<T> collection, IClientSessionHandle session,
        CancellationToken cancellationToken = default);
}