using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Interfaces;

public interface ISessionProvider
{
    public Task<ITransactionSession> BeginTransactionAsync(TransactionOptions? transactionOptions = null,
        CancellationToken cancellationToken = default);

    public Task<ISession> StartSessionAsync(ClientSessionOptions? clientSessionOptions = null,
        CancellationToken cancellationToken = default);
}