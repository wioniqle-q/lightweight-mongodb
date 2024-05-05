using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Interfaces;

public interface IMongoPersistence
{
    ValueTask Initialize(IServiceCollection services, Func<IMongoClient> clientFactory, Func<IMongoDatabase> databaseFactory);
}