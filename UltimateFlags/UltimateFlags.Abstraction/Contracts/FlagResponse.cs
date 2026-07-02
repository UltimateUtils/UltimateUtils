namespace UltimateFlags.Abstraction.Contracts;

public record FlagResponse
{
    public required string Key { get; set; }

    public required bool IsOn { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
}
