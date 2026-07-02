using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions.ClientFaults;

public class FlagNotFound : ClientFault
{
    public override ClientFaultReason Reason => ClientFaultReason.FlagNotFound;
}
