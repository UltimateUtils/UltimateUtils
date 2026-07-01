using HelloUltimateUtils.API.Contracts;

namespace HelloUltimateUtils.API.Services.Abstraction;

public interface IHealthCheckService
{
    public HealthCheckResponse Ping(string? name = null);
}
