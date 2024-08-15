namespace MongoDB.Dev.Core.Interfaces;

public interface ISharedLockAsync
{
    public IAsyncSemaphore AsyncSemaphore { get; }
}