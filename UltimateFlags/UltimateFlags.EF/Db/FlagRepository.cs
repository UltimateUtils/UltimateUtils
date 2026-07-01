using Microsoft.Extensions.Logging;

namespace UltimateFlags.EF.Db;

internal class FlagRepository
{
    private readonly FlagDbContext _flagDbContext;

    public FlagRepository(
        ILogger<FlagDbContext> logger,
        FlagDbContext flagDbContext)
    {
        _flagDbContext = flagDbContext;
    }
}
