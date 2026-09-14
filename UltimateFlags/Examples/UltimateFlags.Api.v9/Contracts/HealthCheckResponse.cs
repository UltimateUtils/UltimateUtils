namespace UltimateFlags.Api.v9.Contracts;

public record HealthCheckResponse
{
    public required string Message { get; init; }
}
