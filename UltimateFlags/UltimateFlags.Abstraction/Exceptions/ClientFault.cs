using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions;

public class ClientFault : UltimateFlagsExceptionBase
{
    public required ClientFaultReason Reason { get; set; }

    public override string GetReason()
    {
        return Reason.ToString();
    }
}
