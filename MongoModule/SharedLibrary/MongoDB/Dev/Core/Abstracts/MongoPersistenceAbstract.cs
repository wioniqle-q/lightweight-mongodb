using Microsoft.Extensions.DependencyInjection;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Abstracts;

public abstract class MongoPersistenceAbstract : IMongoPersistence
{
    public abstract ValueTask Initialize(IServiceCollection services, Func<IMongoClient> clientFactory,
        Func<IMongoDatabase> databaseFactory);
}