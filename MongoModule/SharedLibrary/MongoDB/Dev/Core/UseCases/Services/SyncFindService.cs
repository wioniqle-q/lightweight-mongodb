using System.Data;
using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class SyncFindService
{
    public static IMongoSyncFind<T> SyncFind<T>() where T : IMongoModel
    {
        return new InternalMongoSyncFind<T>();
    }

    private sealed class InternalMongoSyncFind<T> : IMongoSyncFind<T> where T : IMongoModel
    {
        private FilterDefinition<T> _filter = Builders<T>.Filter.Empty;

        public IMongoSyncFind<T> Where(Expression<Func<T, bool>> expression)
        {
            _filter = Builders<T>.Filter.Where(expression);
            return this;
        }

        public async Task<T> SyncFind(IMongoCollection<T> collection,
            IClientSessionHandle session, CancellationToken cancellationToken = default)
        {
            if (session is null)
                throw new NoNullAllowedException("You must provide a session to perform this operation.");
            if (_filter is null)
                throw new NoNullAllowedException("You must provide a filter to perform this operation.");

            using var cursor = collection.Find(session, _filter).FirstOrDefaultAsync(cancellationToken);
            return await cursor;
        }
    }
}