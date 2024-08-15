using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Interfaces;

public interface ISession : IAsyncDisposable
{
    public IClientSessionHandle InnerSession { get; }
}