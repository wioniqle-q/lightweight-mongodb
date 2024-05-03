using MongoDB.Dev.Core.Abstracts;
using MongoDB.Dev.Core.DataObjects;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Extensions;

internal sealed class AsyncSemaphore(SharedLockCollection options) : AsyncSemaphoreAbstract
{
    private readonly AsyncLocal<IAsyncDisposable?> _asyncLocal = new();
    private readonly SemaphoreSlim _semaphore = new(options, options);

    public override async Task<IAsyncDisposable?> WaitAsync(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
            return await Task.FromCanceled<IAsyncDisposable>(cancellationToken);

        var waitTask = _semaphore.WaitAsync(cancellationToken);
        if (waitTask.Status is TaskStatus.RanToCompletion)
        {
            var releaser = new Releaser(this);
            _asyncLocal.Value = releaser;
            return releaser;
        }

        if (cancellationToken.CanBeCanceled is false)
            return await Task.FromResult<IAsyncDisposable>(new Releaser(this));

        var task = new TaskCompletionSource<IAsyncDisposable>(TaskCreationOptions.RunContinuationsAsynchronously);
        cancellationToken.Register(state =>
            {
                var (source, semaphore) = ((TaskCompletionSource<IAsyncDisposable>, SemaphoreSlim))state!;
                source.TrySetCanceled(cancellationToken);
                semaphore.Release();
            },
            (task, _semaphore));

        await waitTask.ContinueWith(
            static (task, state) =>
            {
                var (tcs, semaphore, releaser) =
                    ((TaskCompletionSource<IAsyncDisposable>, SemaphoreSlim, Releaser))state!;
                if (task.Status is not TaskStatus.RanToCompletion || tcs.TrySetResult(releaser) is false) return;
                semaphore.Release();
            },
            (task, _semaphore, new Releaser(this)),
            CancellationToken.None,
            TaskContinuationOptions.ExecuteSynchronously,
            TaskScheduler.Default).ConfigureAwait(false);

        return await task.Task;
    }

    private void Release()
    {
        _semaphore.Release();
    }

    private sealed class Releaser(AsyncSemaphore semaphore) : IReleaser, IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
        }

        private async ValueTask DisposeAsyncCore()
        {
            await (semaphore._asyncLocal.Value?.DisposeAsync().AsTask() ?? Task.CompletedTask);
            semaphore._asyncLocal.Value = null;
            semaphore.Release();
        }
    }
}