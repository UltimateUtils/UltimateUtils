using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Helpers;

public static class FlagHelper
{
    internal static Flag UpdateFrom(this Flag entity, FlagUpdateRequest contract)
    {
        entity.Name = contract.Name;

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
