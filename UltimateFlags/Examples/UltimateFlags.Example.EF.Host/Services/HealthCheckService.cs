using Microsoft.Extensions.Options;
using UltimateFlags.Example.EF.Host.Config;
using UltimateFlags.Example.EF.Host.Contracts;
using UltimateFlags.Example.EF.Host.Services.Abstraction;

namespace UltimateFlags.Example.EF.Host.Services;

public class HealthCheckService : IHealthCheckService
{
    private readonly ILogger<HealthCheckService> _logger;

    private readonly ServiceConfiguration _serviceConfiguration;

    public HealthCheckService(
        ILogger<HealthCheckService> logger,
        IOptions<ServiceConfiguration> options)
    {
        _logger = logger;
        _serviceConfiguration = options.Value;
    }

    public HealthCheckResponse Ping(string? name = null)
    {
        return new HealthCheckResponse
        {
            Message = $"Pong, {name ?? "there"} (from {_serviceConfiguration.ServiceName})",
        };
    }
}
