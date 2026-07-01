namespace HelloUltimateUtils.API.Contracts;

public record HealthCheckResponse
{
    public required string Message { get; init; }
}
