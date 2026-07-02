namespace UltimateFlags.Example.EF.Host.Contracts;

public record HealthCheckResponse
{
    public required string Message { get; init; }
}
