using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenRouterSharp.Core.InfraServices;
using OpenRouterSharp.Core.Repositories;
using SearchiaSharp.AspNetCore.Config;

namespace SearchiaSharp.AspNetCore.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenRouterService(this IServiceCollection services, Action<SearchiaSetting> configure)
    {
        services.Configure(configure);

        services.AddScoped((Func<IServiceProvider, Isearchioa>)(sp =>
        {
            var setting = sp.GetRequiredService<IOptionsMonitor<SearchiaSetting>>().CurrentValue;
            return new OpenRouterService(
                setting.BaseUrl,
                setting.ApiKey
            );
        }));

        return services;
    }
}

