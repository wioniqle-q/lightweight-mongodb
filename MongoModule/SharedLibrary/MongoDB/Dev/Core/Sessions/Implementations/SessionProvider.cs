using MongoDB.Dev.Core.Sessions.Abstractions;
using MongoDB.Dev.Core.Sessions.Interfaces;
using MongoDB.Driver;

namespace MongoDB.Dev.Core.Sessions.Implementations;

public sealed class SessionProvider(IMongoClient client) : SessionProviderAbstract
{
    private static readonly TransactionOptions TransactionOptions = new(
        ReadConcern.Majority.With(ReadConcernLevel.Majority),
        ReadPreference.PrimaryPreferred.With(ReadPreferenceMode.Primary),
        WriteConcern.WMajority.With(journal: true),
        TimeSpan.FromSeconds(30));

    private static readonly ClientSessionOptions ClientSessionOptions = new()
    {
        CausalConsistency = true,
        DefaultTransactionOptions = new TransactionOptions(
            ReadConcern.Majority.With(ReadConcernLevel.Majority),
            ReadPreference.PrimaryPreferred.With(ReadPreferenceMode.Primary),
            WriteConcern.WMajority.With(journal: true),
            TimeSpan.FromSeconds(30))
    };

    public override async Task<ITransactionSession> BeginTransactionAsync(
        TransactionOptions? transactionOptions = null, CancellationToken cancellationToken = default)
    {
        var sessionHandle = await client.StartSessionAsync(cancellationToken: cancellationToken);
        sessionHandle.StartTransaction(transactionOptions ?? TransactionOptions);
        return new TransactionSession(sessionHandle);
    }
    
    public override async Task<ISession> StartSessionAsync(ClientSessionOptions? clientSessionOptions = null,
        CancellationToken cancellationToken = default)
    {
        var sessionHandle = await client.StartSessionAsync(clientSessionOptions ?? ClientSessionOptions, cancellationToken);
        return new PersistentSession(sessionHandle);
    }
}