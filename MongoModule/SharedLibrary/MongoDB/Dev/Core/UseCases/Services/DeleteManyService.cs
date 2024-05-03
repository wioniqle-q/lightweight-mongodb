using System.Data;
using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class DeleteManyService
{
    public static IMongoDeleteMany<T> DeleteManyAsync<T>() where T : IMongoModel
    {
        return new InternalMongoDeleteMany<T>();
    }

    private sealed class InternalMongoDeleteMany<T> : IMongoDeleteMany<T> where T : IMongoModel
    {
        private FilterDefinition<T>? _filter;

        public IMongoDeleteMany<T> Where(Expression<Func<T, bool>> expression)
        {
            _filter = Builders<T>.Filter.Where(expression);
            return this;
        }

        public async Task<T> DeleteManyAsync(IMongoCollection<T> collection,
            CancellationToken cancellationToken = default)
        {
            if (_filter is null)
                throw new NoNullAllowedException("You must provide a filter to perform this operation.");

            var result = await collection.DeleteManyAsync(_filter, cancellationToken);
            if (result.DeletedCount > 0)
                throw new Exception("No documents were deleted.");

            return default!;
        }
    }
}