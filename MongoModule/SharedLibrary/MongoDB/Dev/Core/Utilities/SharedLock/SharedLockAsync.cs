using MongoDB.Dev.Core.Abstracts;
using MongoDB.Dev.Core.DataObjects;
using MongoDB.Dev.Core.Extensions;
using MongoDB.Dev.Core.Interfaces;

namespace MongoDB.Dev.Core.Utilities.SharedLock;

public sealed class SharedLockAsync(SharedLockCollection options) : SharedLockAsyncAbstract
{
    public override IAsyncSemaphore AsyncSemaphore { get; } = new AsyncSemaphore(options.Value);
}