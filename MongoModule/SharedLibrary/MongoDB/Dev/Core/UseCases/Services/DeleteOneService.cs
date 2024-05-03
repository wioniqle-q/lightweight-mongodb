using System.Data;
using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class DeleteOneService
{
    public static IMongoDeleteOne<T> DeleteOneAsync<T>() where T : IMongoModel
    {
        return new InternalMongoDeleteOne<T>();
    }

    private sealed class InternalMongoDeleteOne<T> : IMongoDeleteOne<T> where T : IMongoModel
    {
        private FilterDefinition<T>? _filter;

        public IMongoDeleteOne<T> Where(Expression<Func<T, bool>> expression)
        {
            _filter = Builders<T>.Filter.Where(expression);
            return this;
        }

        public async Task<T> DeleteOneAsync(IMongoCollection<T> collection,
            CancellationToken cancellationToken = default)
        {
            if (_filter is null)
                throw new NoNullAllowedException("You must provide a filter to perform this operation.");

            var result = await collection.DeleteOneAsync(_filter, cancellationToken);
            if (result.DeletedCount > 0)
                return default!;

            throw new Exception("No document was deleted.");
        }
    }
}