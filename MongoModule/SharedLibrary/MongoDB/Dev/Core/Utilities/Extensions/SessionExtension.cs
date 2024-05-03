using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Dev.Core.Sessions.Implementations;
using MongoDB.Dev.Core.Sessions.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Utilities.Extensions;

public static class SessionExtension
{
    public static void AddMongoSession(this IServiceCollection services,
        Func<IClientSessionHandle> clientSessionFactory)
    {
        services.TryAddScoped<ISessionProvider, SessionProvider>();

        services.TryAddSingleton<ISession, PersistentSession>();
        services.TryAddSingleton<ITransactionSession, TransactionSession>();

        services.TryAddSingleton<IClientSessionHandle>(_ => clientSessionFactory());
    }
}