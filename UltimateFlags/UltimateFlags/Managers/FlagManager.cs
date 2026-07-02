using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;

namespace UltimateFlags.Managers;

internal class FlagManager : IFlagManager
{
    private readonly ILogger<FlagManager> _logger;

    private readonly IFlagStorage _storage;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagManager(
        ILogger<FlagManager> logger,
        IFlagStorage storage,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _storage = storage;
        _ultimateFlagConfiguration = options.Value;
    }

    #region sync

    public Flag Create(string name, Guid? parentId, bool isOn)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        return _storage.SaveChanges();
    }

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(
        string name,
        Guid? parentId,
        bool isOn,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> GetAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _storage.SaveChangesAsync(cancellationToken);
    }

    #endregion async
}
