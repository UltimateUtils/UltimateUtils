using System.Diagnostics.CodeAnalysis;

namespace UltimateFlags.Abstraction.Contracts;

public record FlagRequest
{
    // todo - key/name에 들어갈 수 있는 문자 제한
    public required string Key { get; set; }

    public required bool IsOn { get; set; }
}
