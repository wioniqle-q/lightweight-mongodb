using MongoDB.Dev.Core.Sessions.Abstractions;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Implementations;

internal sealed class TransactionSession(IClientSessionHandle clientSession)
    : TransactionSessionAbstract, IAsyncDisposable
{
    private bool _disposed;
    public override IClientSessionHandle Session { get; } = clientSession;

    public override async ValueTask DisposeAsync()
    {
        if (_disposed is not true)
        {
            await DisposeAsyncCore();
            _disposed = true;
        }
    }

    public override async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Session.CommitTransactionAsync(cancellationToken);
    }

    public override async Task AbortTransactionAsync(CancellationToken cancellationToken = default)
    {
        await Session.AbortTransactionAsync(cancellationToken);
    }

    private Task DisposeAsyncCore()
    {
        Session.Dispose();
        return Task.CompletedTask;
    }
}