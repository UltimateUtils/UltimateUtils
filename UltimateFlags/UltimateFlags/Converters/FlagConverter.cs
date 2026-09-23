using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Converters;

internal static class FlagConverter
{
    internal static Flag ToEntity(this FlagCreationRequest creationRequest)
    {
        DateTime utcNow = DateTime.UtcNow;

        return
            new Flag
            {
                Id = Guid.NewGuid(),
                Name = creationRequest.Name,
                ParentId = creationRequest.ParentId,
                IsOn = creationRequest.IsOn,
                Description = creationRequest.Description,
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
