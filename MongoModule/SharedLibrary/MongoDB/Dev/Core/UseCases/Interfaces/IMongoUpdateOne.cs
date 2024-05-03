using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoUpdateOne<T> where T : IMongoModel
{
    public IMongoUpdateOne<T> Where(Expression<Func<T, bool>> expression);
    public IMongoUpdateOne<T> Set(UpdateDefinition<T> updateDefinition);

    public Task<T> UpdateOneAsync(IMongoCollection<T> collection, IClientSessionHandle session,
        CancellationToken cancellationToken = default);
}