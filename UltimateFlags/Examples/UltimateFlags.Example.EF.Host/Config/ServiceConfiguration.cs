namespace UltimateFlags.Example.EF.Host.Config;

public record ServiceConfiguration
{
    public const string SectionName = "ServiceConfiguration";

    public required string ServiceName { get; set; }
}
