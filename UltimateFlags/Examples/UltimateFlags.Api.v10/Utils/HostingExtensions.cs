using Microsoft.EntityFrameworkCore;
using UltimateFlags.Api.v10.Config;
using UltimateFlags.Api.v10.Db;
using UltimateFlags.Api.v10.Services;
using UltimateFlags.Api.v10.Services.Abstraction;
using UltimateFlags.EF.DI;

namespace UltimateFlags.Api.v10.Utils;

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
            app.UseSwaggerUi(
                options =>
                {
                    options.DocumentPath = "/openapi/v1.json";
                });
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
        services.AddTransient<IFlagService, FlagService>();
        services.AddTransient<UltimateFlags.Abstraction.Services.IFlagService, UltimateFlags.Services.FlagService>();
        services.AddUltimateFlags<MyFlagDbContext>(
            configuration,
            options => options.UseSqlite("name=ConnectionStrings:MyFlagsDb"));
    }
}
