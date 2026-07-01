using HelloUltimateUtils.API.Contracts;
using HelloUltimateUtils.API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace HelloUltimateUtils.API.Controllers;

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
