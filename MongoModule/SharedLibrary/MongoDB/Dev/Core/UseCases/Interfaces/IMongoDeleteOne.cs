using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoDeleteOne<T> where T : IMongoModel
{
    public IMongoDeleteOne<T> Where(Expression<Func<T, bool>> expression);
    public Task<T> DeleteOneAsync(IMongoCollection<T> collection, CancellationToken cancellationToken = default);
}