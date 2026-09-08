using HelloUltimateUtils.API.Config;
using HelloUltimateUtils.API.Db;
using HelloUltimateUtils.API.Services;
using HelloUltimateUtils.API.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using UltimateFlags.EF.DI;

namespace HelloUltimateUtils.API.Utils;

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
        services.AddUltimateFlags<HelloDbContext>(
            configuration,
            options => options.UseNpgsql("name=ConnectionStrings:UltimateFlagsDb"));
    }
}
