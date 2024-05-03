using System.Data;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.UseCases.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.UseCases.Services;

public static class InsertOneService
{
    public static IMongoInsertOne<T> InsertOneAsync<T>(IMongoCollection<T> collection, T model,
        IClientSessionHandle session,
        CancellationToken cancellationToken = default) where T : IMongoModel
    {
        return new InternalMongoInsertOne<T>();
    }

    private sealed class InternalMongoInsertOne<T> : IMongoInsertOne<T> where T : IMongoModel
    {
        public async Task<T> InsertOneAsync(IMongoCollection<T> collection, T model, IClientSessionHandle session,
            CancellationToken cancellationToken = default)
        {
            if (session is null)
                throw new NoNullAllowedException("You must provide a session to perform this operation.");

            await collection.InsertOneAsync(session, model, cancellationToken: cancellationToken);
            return model;
        }
    }
}