namespace UltimateFlags.Abstraction.Contracts;

public record FlagResponse
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public bool IsOn { get; set; }
}
