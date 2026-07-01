using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltimateFlags.DI;
using UltimateFlags.EF.Db;
using UltimateFlags.EF.Storages;

namespace UltimateFlags.EF.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUltimateFlags(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<DbContextOptionsBuilder>? optionsAction)
    {
        services.AddDbContext<FlagDbContext>(optionsAction);
        services.AddScoped<FlagRepository>();
        services.AddUltimateFlags<FlagStorage>(configuration);

        return services;
    }
}
