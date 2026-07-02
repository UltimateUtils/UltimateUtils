namespace UltimateFlags.Abstraction.Entities;

public record Flag
{
    public Guid Id { get; init; }

    // todo - name에 들어갈 수 있는 문자 제한
    public required string Name { get; set; }

    public required bool IsOn { get; set; }

    public required DateTime CreatedAt { get; init; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime? DeletedAt { get; set; }

    #region relations

    public required Guid? ParentId { get; init; }

    public Flag? Parent { get; set; }

    #endregion relations
}
