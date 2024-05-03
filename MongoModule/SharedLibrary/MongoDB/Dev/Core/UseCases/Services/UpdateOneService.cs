using System.Data;
using System.Linq.Expressions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class UpdateOneService
{
    public static IMongoUpdateOne<T> UpdateOneAsync<T>() where T : IMongoModel
    {
        return new InternalMongoUpdateOne<T>();
    }

    private sealed class InternalMongoUpdateOne<T> : IMongoUpdateOne<T> where T : IMongoModel
    {
        private FilterDefinition<T> _filter = Builders<T>.Filter.Empty;
        private UpdateDefinition<T> _update = Builders<T>.Update.Set(string.Empty, string.Empty);

        public IMongoUpdateOne<T> Where(Expression<Func<T, bool>> expression)
        {
            _filter = Builders<T>.Filter.Where(expression);
            return this;
        }

        public IMongoUpdateOne<T> Set(UpdateDefinition<T> updateDefinition)
        {
            _update = updateDefinition;
            return this;
        }

        public async Task<T> UpdateOneAsync(IMongoCollection<T> collection, IClientSessionHandle session,
            CancellationToken cancellationToken = default)
        {
            if (session is null)
                throw new NoNullAllowedException("You must provide a session to perform this operation.");
            if (_filter is null)
                throw new NoNullAllowedException("You must provide a filter to perform this operation.");
            if (_update is null)
                throw new NoNullAllowedException("You must provide an update to perform this operation.");

            var task = collection.UpdateOneAsync(session, _filter, _update, cancellationToken: cancellationToken);
            await task;

            return await collection.Find(session, _filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}