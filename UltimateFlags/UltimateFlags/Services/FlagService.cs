using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Services;
using UltimateFlags.Managers;

namespace UltimateFlags.Services;

public class FlagService : IFlagService
{
    private readonly ILogger<FlagService> _logger;

    private readonly IFlagManager _flagManager;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagService(
        ILogger<FlagService> logger,
        IFlagManager flagManager,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _flagManager = flagManager;
        _ultimateFlagConfiguration = options.Value;
    }

    #region sync

    public FlagResponse Create(FlagCreationRequest contract)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Read(string key)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlagResponse> List()
    {
        throw new NotImplementedException();
    }

    public FlagResponse Update(string key, FlagUpdateRequest contract)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Delete(string key)
    {
        throw new NotImplementedException();
    }

    public void Enable(string key)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string key)
    {
        throw new NotImplementedException();
    }

    #endregion sync

    #region async

    public Task<FlagResponse> CreateAsync(FlagCreationRequest flag, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> UpdateAsync(string key, FlagUpdateRequest contract, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion async
}
