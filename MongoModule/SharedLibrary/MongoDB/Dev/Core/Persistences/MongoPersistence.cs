using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Dev.Core.Abstracts;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Persistences;

public sealed class MongoPersistence : MongoPersistenceAbstract
{
    public override async ValueTask Initialize(IServiceCollection services, Func<IMongoClient> clientFactory,
        Func<IMongoDatabase> databaseFactory)
    {
        if (services is null || clientFactory is null || databaseFactory is null)
            throw new ArgumentNullException(nameof(services));

        try
        {
            await InitAsync(services, clientFactory, databaseFactory);
        }
        catch (Exception ex)
        {
            throw new Exception($"MongoDB persistence initialization failed: {ex.Message}", ex);
        }
    }

    private static ValueTask InitAsync(IServiceCollection services, Func<IMongoClient> clientFactory,
        Func<IMongoDatabase> databaseFactory)
    {
        services.TryAddSingleton<IMongoClient>(_ => clientFactory());
        services.TryAddSingleton<IMongoDatabase>(_ => databaseFactory());
        return default;
    }
}