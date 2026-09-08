using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Services;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.Managers;
using UltimateFlags.Services;
using UltimateFlags.Storages;

namespace UltimateFlags.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUltimateFlags(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection configurationSection = configuration.GetSection(UltimateFlagConfiguration.SectionName);

        if (configurationSection.Exists())
            services.Configure<UltimateFlagConfiguration>(configurationSection);

        services.AddScoped<IFlagStorage, FlagStorage>();
        services.AddScoped<IFlagManager, FlagManager>();
        services.AddScoped<IFlagService, FlagService>();

        return services;
    }

    public static IServiceCollection AddUltimateFlags<T>(this IServiceCollection services, IConfiguration configuration)
        where T : class, IFlagStorage
    {
        IConfigurationSection configurationSection = configuration.GetSection(UltimateFlagConfiguration.SectionName);

        if (configurationSection.Exists())
            services.Configure<UltimateFlagConfiguration>(configurationSection);

        services.AddScoped<IFlagStorage, T>();
        services.AddScoped<IFlagManager, FlagManager>();
        services.AddScoped<IFlagService, FlagService>();

        return services;
    }
}
