using MongoDB.Dev.Core.Sessions.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Abstractions;

public abstract class SessionProviderAbstract : ISessionProvider
{
    public abstract Task<ITransactionSession> BeginTransactionAsync(TransactionOptions? transactionOptions = null,
        CancellationToken cancellationToken = default);

    public abstract Task<ISession> StartSessionAsync(ClientSessionOptions? clientSessionOptions = null,
        CancellationToken cancellationToken = default);
}