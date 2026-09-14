using Microsoft.AspNetCore.Mvc;
using UltimateFlags.Api.v8.Contracts;
using UltimateFlags.Api.v8.Services.Abstraction;

namespace UltimateFlags.Api.v8.Controllers;

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
