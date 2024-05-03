using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Persistences;

public sealed class MongoPersistence(ISharedLockAsync sharedLockAsync)
{
    public async ValueTask Initialize(IServiceCollection services, Func<IMongoClient> clientFactory,
        Func<IMongoDatabase> databaseFactory)
    {
        try
        {
            await using (await sharedLockAsync.AsyncSemaphore.WaitAsync())
            {
                await InitAsync(services, clientFactory, databaseFactory);
            }
        }
        catch (Exception exception) when (exception is TaskCanceledException)
        {
            throw new Exception($"MongoDB persistence initialization failed due to a canceled task operation.\n\n" +
                                $"Error details:\n{exception}\n\n" +
                                "Potential causes:\n" +
                                "- An issue occurred while acquiring an asynchronous lock.\n" +
                                "- The task was canceled prematurely.\n" +
                                "- The task was disposed unexpectedly.\n" +
                                "- The task encountered an unhandled exception.\n" +
                                "- The mongodb connection failed.\n\n" +
                                "Please investigate the root cause, ensure proper synchronization, and implement appropriate task management strategies.");
        }
        catch (Exception exception)
        {
            throw new Exception($"MongoDB persistence initialization failed.\n\n" +
                                $"Error details:\n{exception}\n\n" +
                                "Potential causes:\n" +
                                "- An issue occurred while initializing the MongoDB connection.\n" +
                                "- The MongoDB connection failed.\n\n" +
                                "Please investigate the root cause and ensure proper MongoDB connection settings.");
        }
    }

    private static ValueTask InitAsync(IServiceCollection services, Func<IMongoClient> clientFactory,
        Func<IMongoDatabase> databaseFactory)
    {
        try
        {
            services.TryAddSingleton<IMongoClient>(_ => clientFactory());
            services.TryAddSingleton<IMongoDatabase>(_ => databaseFactory());
        }
        catch (Exception exception)
        {
            throw new Exception("MongoDB connection failed." + exception);
        }

        return default;
    }
}