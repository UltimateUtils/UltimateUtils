using Microsoft.EntityFrameworkCore;
using UltimateFlags.EF.DI;
using UltimateFlags.Example.EF.Host.Config;
using UltimateFlags.Example.EF.Host.Db;
using UltimateFlags.Example.EF.Host.Services;
using UltimateFlags.Example.EF.Host.Services.Abstraction;

namespace UltimateFlags.Example.EF.Host.Utils;

internal static class HostingExtensions
{
    internal static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder
                .Services
                .AddLogging(
                    loggingBuilder =>
                    {
                        loggingBuilder.AddSeq();
                    });
        }

        // Add services to the container.
        builder.Services._AddServiceDependencies(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        return builder.Build();
    }

    internal static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    private static void _AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceConfiguration>(configuration.GetRequiredSection(ServiceConfiguration.SectionName));

        services.AddTransient<IHealthCheckService, HealthCheckService>();
        services.AddUltimateFlags<ExampleDbContext>(
            configuration,
            options => options.UseSqlite("name=ConnectionStrings:UltimateFlagsDb"));
    }
}
