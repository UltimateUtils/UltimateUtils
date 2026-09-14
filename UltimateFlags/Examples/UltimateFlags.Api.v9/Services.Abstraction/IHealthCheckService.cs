using UltimateFlags.Api.v9.Contracts;

namespace UltimateFlags.Api.v9.Services.Abstraction;

public interface IHealthCheckService
{
    public HealthCheckResponse Ping(string? name = null);
}
