using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Interfaces;

public interface ITransactionSession : IAsyncDisposable
{
    public IClientSessionHandle Session { get; }
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task AbortTransactionAsync(CancellationToken cancellationToken = default);
}