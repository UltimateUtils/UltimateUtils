using UltimateFlags.Api.v8.Contracts;

namespace UltimateFlags.Api.v8.Services.Abstraction;

public interface IHealthCheckService
{
    public HealthCheckResponse Ping(string? name = null);
}
