namespace MongoDB.Dev.Core.Interfaces;

public interface IAsyncSemaphore
{
    public Task<IAsyncDisposable?> WaitAsync(CancellationToken cancellationToken = default);
}