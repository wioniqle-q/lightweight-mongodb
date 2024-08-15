using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Dev.Core.Interfaces;
using MongoDB.Dev.Core.Providers;

namespace MongoDB.Dev.Core.Extensions;

public static class ProviderExtension
{
    public static void AddProvider(this IServiceCollection services)
    {
        services.TryAddSingleton<IModelOverviewProvider, ModelOverviewProvider>();
        services.TryAddSingleton<IModelOverviewProviderCache, ModelOverviewProviderCache>();
    }
}