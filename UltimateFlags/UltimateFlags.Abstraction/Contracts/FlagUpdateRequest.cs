namespace UltimateFlags.Abstraction.Contracts;

public record FlagUpdateRequest
{
    // todo - Name에 들어갈 수 있는 문자 종류 제한
    public string? Name { get; init; }

    public bool? IsOn { get; init; }

    public string? Description { get; init; }
}
