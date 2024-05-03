using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Abstracts;

internal abstract class AsyncSemaphoreAbstract : IAsyncSemaphore
{
    public abstract Task<IAsyncDisposable?> WaitAsync(CancellationToken cancellationToken = default);
}