using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Helpers;

public static class FlagHelper
{
    internal static Flag UpdateFrom(this Flag entity, FlagUpdateRequest contract)
    {
        entity.Name = contract.Key; // todo - shong
        entity.IsOn = contract.IsOn;

        return entity;
    }
}
