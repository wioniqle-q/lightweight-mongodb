using System.Data;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class InsertManyService
{
    public static IMongoInsertMany<T> InsertMany<T>() where T : IMongoModel
    {
        return new InternalMongoInsertMany<T>();
    }

    private sealed class InternalMongoInsertMany<T> : IMongoInsertMany<T> where T : IMongoModel
    {
        public async Task<IEnumerable<T>> InsertManyAsync(IMongoCollection<T> collection, IEnumerable<T> models,
            IClientSessionHandle session, CancellationToken cancellationToken = default)
        {
            if (session is null)
                throw new NoNullAllowedException("You must provide a session to perform this operation.");

            var manyAsync = models as T[] ?? models.ToArray();
            await collection.InsertManyAsync(session, manyAsync, cancellationToken: cancellationToken);

            return manyAsync;
        }
    }
}