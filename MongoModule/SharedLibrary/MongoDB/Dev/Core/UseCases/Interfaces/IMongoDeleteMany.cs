using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Interfaces;

public interface IMongoDeleteMany<T> where T : IMongoModel
{
    public IMongoDeleteMany<T> Where(Expression<Func<T, bool>> expression);
    public Task<T> DeleteManyAsync(IMongoCollection<T> collection, CancellationToken cancellationToken = default);
}