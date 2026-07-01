namespace UltimateFlags.Abstraction.Entities;

public record Flag
{
    public required string Key { get; set; }

    public required bool IsOn { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
