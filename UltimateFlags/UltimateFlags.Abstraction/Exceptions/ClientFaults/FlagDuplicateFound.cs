using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions.ClientFaults;

public class FlagDuplicateFound : ClientFault
{
    public override ClientFaultReason Reason => ClientFaultReason.FlagDuplicateFound;
}
