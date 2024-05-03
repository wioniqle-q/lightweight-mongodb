using System.Data;
using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class AsyncFindService
{
    public static IMongoAsyncFind<T> AsyncFind<T>() where T : IMongoModel
    {
        return new InternalMongoAsyncFind<T>();
    }

    private sealed class InternalMongoAsyncFind<T> : IMongoAsyncFind<T> where T : IMongoModel
    {
        private FilterDefinition<T> _filter = Builders<T>.Filter.Empty;
        private FindOptions<T> _options = new();

        public IMongoAsyncFind<T> WithOptions(FindOptions<T> options)
        {
            _options = options;
            return this;
        }

        public IMongoAsyncFind<T> Where(Expression<Func<T, bool>> expression)
        {
            _filter = Builders<T>.Filter.Where(expression);
            return this;
        }

        public async Task<T> AsyncFind(IMongoCollection<T> collection, IClientSessionHandle session,
            CancellationToken cancellationToken = default)
        {
            if (session is null)
                throw new NoNullAllowedException("You must provide a session to perform this operation.");
            if (_filter is null)
                throw new NoNullAllowedException("You must provide a filter to perform this operation.");

            using var cursor = await collection.FindAsync(session, _filter, _options, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}