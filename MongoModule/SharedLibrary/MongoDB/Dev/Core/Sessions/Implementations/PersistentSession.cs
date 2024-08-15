using MongoDB.Dev.Core.Sessions.Abstractions;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Implementations;

internal sealed class PersistentSession(IClientSessionHandle session) : SessionAbstract, IAsyncDisposable
{
    private bool _disposed;
    public override IClientSessionHandle InnerSession { get; } = session;

    public override async ValueTask DisposeAsync()
    {
        if (_disposed is not true)
        {
            await DisposeAsyncCore();
            _disposed = true;
        }
    }

    private Task DisposeAsyncCore()
    {
        InnerSession.Dispose();
        return Task.CompletedTask;
    }
}