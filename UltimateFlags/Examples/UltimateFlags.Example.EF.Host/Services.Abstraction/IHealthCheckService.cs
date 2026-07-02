using UltimateFlags.Example.EF.Host.Contracts;

namespace UltimateFlags.Example.EF.Host.Services.Abstraction;

public interface IHealthCheckService
{
    public HealthCheckResponse Ping(string? name = null);
}
