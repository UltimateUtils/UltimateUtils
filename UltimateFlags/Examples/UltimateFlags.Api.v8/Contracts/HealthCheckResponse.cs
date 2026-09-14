namespace UltimateFlags.Api.v8.Contracts;

public record HealthCheckResponse
{
    public required string Message { get; init; }
}
