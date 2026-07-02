using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Converters;

internal static class FlagConverter
{
    internal static Flag ToEntity(this FlagCreationRequest contract)
    {
        throw new NotImplementedException();
    }

    internal static FlagResponse ToContract(this Flag entity)
    {
        throw new NotImplementedException();
    }

    internal static IEnumerable<FlagResponse> ToContracts(this IEnumerable<Flag> entities)
    {
        return entities.Select(ToContract);
    }
}
