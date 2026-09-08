using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions.ClientFaults;

public class FlagParentDeleted : ClientFault
{
    protected override ClientFaultReason Reason => ClientFaultReason.FlagParentDeleted;
}
