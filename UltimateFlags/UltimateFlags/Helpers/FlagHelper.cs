using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Helpers;

public static class FlagHelper
{
    internal static Flag UpdateFrom(this Flag entity, FlagUpdateRequest contract)
    {
        if (contract.Name is not null && entity.Name != contract.Name)
            entity.Name = contract.Name;

        if (contract.Description is not null && entity.Description != contract.Description)
            entity.Description = contract.Description;

        if (contract.IsOn.HasValue && entity.IsOn != contract.IsOn.Value)
            entity.IsOn = contract.IsOn.Value;

        entity.UpdatedAt = DateTime.UtcNow;

        return entity;
    }

    internal static Flag Enabled(this Flag entity)
    {
        entity.IsOn = true;

        return entity;
    }

    internal static Flag Disabled(this Flag entity)
    {
        entity.IsOn = false;

        return entity;
    }
}
