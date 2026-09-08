using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Converters;

internal static class FlagConverter
{
    internal static Flag ToEntity(this FlagCreationRequest contract)
    {
        DateTime utcNow = DateTime.UtcNow;

        return
            new Flag
            {
                Id = Guid.NewGuid(),
                Name = contract.Name,
                ParentId = contract.ParentId,
                IsOn = contract.IsOn,
                Description = contract.Description,
                CreatedAt = utcNow,
                UpdatedAt = utcNow,
                DeletedAt = null,
            };
    }

    internal static FlagResponse ToContract(this Flag entity)
    {
        return
            new FlagResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,
                IsOn = entity.IsOn,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
    }

    internal static IEnumerable<FlagResponse> ToContracts(this IEnumerable<Flag> entities)
    {
        return entities.Select(ToContract);
    }
}
