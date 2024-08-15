using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Abstracts;

public abstract class SharedLockAsyncAbstract : ISharedLockAsync
{
    public abstract IAsyncSemaphore AsyncSemaphore { get; }
}