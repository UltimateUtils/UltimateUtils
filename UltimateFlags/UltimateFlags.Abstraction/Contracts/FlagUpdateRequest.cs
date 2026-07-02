namespace UltimateFlags.Abstraction.Contracts;

public record FlagUpdateRequest : FlagRequest
{
    public required string Name { get; set; }
}
