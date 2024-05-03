using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoAsyncFind<T> where T : IMongoModel
{
    public IMongoAsyncFind<T> WithOptions(FindOptions<T> options);
    public IMongoAsyncFind<T> Where(Expression<Func<T, bool>> expression);

    public Task<T> AsyncFind(IMongoCollection<T> collection, IClientSessionHandle session,
        CancellationToken cancellationToken = default);
}