using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions.ClientFaults;

public class PaginationInfoInvalid : ClientFault
{
    public override ClientFaultReason Reason => ClientFaultReason.PaginationInfoInvalid;
}
