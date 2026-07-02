using UltimateFlags.Abstraction.Exceptions.Reasons;

namespace UltimateFlags.Abstraction.Exceptions;

public class ServerFault : UltimateFlagsExceptionBase
{
    public ServerFaultReason Reason { get; set; }

    public override string GetReason()
    {
        return Reason.ToString();
    }
}
