using MongoDB.Dev.Core.Sessions.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Abstractions;

internal abstract class SessionAbstract : ISession
{
    public abstract IClientSessionHandle InnerSession { get; }
    public abstract ValueTask DisposeAsync();
}