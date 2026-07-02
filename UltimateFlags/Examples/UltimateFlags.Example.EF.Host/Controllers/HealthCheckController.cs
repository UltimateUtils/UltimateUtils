using Microsoft.AspNetCore.Mvc;
using UltimateFlags.Example.EF.Host.Contracts;
using UltimateFlags.Example.EF.Host.Services.Abstraction;

namespace UltimateFlags.Example.EF.Host.Controllers;

[ApiController]
[Route("health-check")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    private readonly IHealthCheckService _healthCheckService;

    public HealthCheckController(
        ILogger<HealthCheckController> logger,
        IHealthCheckService healthCheckService)
    {
        _logger = logger;
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    [Route("")]
    [EndpointName("HealthCheck")]
    public HealthCheckResponse Ping([FromQuery] string? name = null)
    {
        return _healthCheckService.Ping(name);
    }
}
