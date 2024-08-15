namespace MongoDB.Dev.Core.Interfaces;

internal interface IReleaser
{
    public ValueTask DisposeAsync();
}