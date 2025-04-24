using MongoDB.Dev.Core.Sessions.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Abstractions;

internal abstract class TransactionSessionAbstract : ITransactionSession
{
    public abstract IClientSessionHandle Session { get; }
    public abstract ValueTask DisposeAsync();

    public abstract Task CommitTransactionAsync(
        CancellationToken cancellationToken = default);

    public abstract Task AbortTransactionAsync(
        CancellationToken cancellationToken = default);
}