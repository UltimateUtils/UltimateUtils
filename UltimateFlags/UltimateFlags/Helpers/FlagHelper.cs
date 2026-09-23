using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Helpers;

public static class FlagHelper
{
    public static Flag UpdatedFrom(this Flag entity, FlagUpdateRequest updateRequest)
    {
        // todo - make it better

        bool updated = false;

        if (updateRequest.Name is not null && entity.Name != updateRequest.Name)
        {
            updated = true;
            entity.Name = updateRequest.Name;
        }

        if (updateRequest.Description is not null && entity.Description != updateRequest.Description)
        {
            updated = true;
            entity.Description = updateRequest.Description;
        }

        if (updateRequest.IsOn.HasValue && entity.IsOn != updateRequest.IsOn.Value)
        {
            updated = true;
            entity.IsOn = updateRequest.IsOn.Value;
        }

        if (updated)
            entity.UpdatedAt = DateTime.UtcNow;

        return entity;
    }

    public static Flag Enabled(this Flag entity)
    {
        entity.IsOn = true;

        return entity;
    }

    public static Flag Disabled(this Flag entity)
    {
        entity.IsOn = false;

        return entity;
    }

    public static Flag Deleted(this Flag entity)
    {
        entity.DeletedAt = DateTime.UtcNow;

        return entity;
    }
}
