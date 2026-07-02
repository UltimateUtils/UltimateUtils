using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.Reasons;
using UltimateFlags.Abstraction.Services;
using UltimateFlags.Managers;
using UltimateFlags.Utils;

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

    public FlagResponse Create(FlagCreationRequest contract)
    {
        return _create(null, contract.Key, contract.IsOn);

        throw new FlagDuplicateFound
        {
            Area = $"{nameof(FlagService)}.{nameof(Create)}(FlagCreationRequest contract)",
            Reason = ClientFaultReason.FlagDuplicateFound,
        };

        FlagResponse _create(Guid? parentId, string key, bool isOn)
        {
            // todo - base case

            // todo - edge cases with '.'
            int firstDot = key.IndexOf('.');

            string name = key[..firstDot];
            Flag? flag = _flagManager.Get(name, parentId);

            if (flag is null)
            {
                Flag parent = _flagManager.Create(name, parentId, isOn);
                return _create(parent.Id, key.Substring(firstDot + 1), isOn);
            }
            else
            {
                return _create(flag.Id, key.Substring(firstDot + 1), isOn);
            }
        }
    }

    public FlagResponse? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public FlagResponse GetRequired(Guid id)
    {
        throw new NotImplementedException();
    }

    public FlagResponse? Get(string key)
    {
        throw new NotImplementedException();
    }

    public FlagResponse GetRequired(string key)
    {
        throw new NotImplementedException();
    }

    public FlagResponse? Get(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public FlagResponse GetRequired(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public FlagResponse? Get(string name, string? parentKey)
    {
        throw new NotImplementedException();
    }

    public FlagResponse GetRequired(string name, string? parentKey)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlagResponse> List(string? searchString)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlagResponse> List(string? searchString, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<FlagResponse> List(string? searchString, string? parentKey)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Update(Guid id, FlagUpdateRequest contract)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Update(string key, FlagUpdateRequest contract)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public FlagResponse Delete(string key)
    {
        throw new NotImplementedException();
    }

    public void Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Enable(string key)
    {
        throw new NotImplementedException();
    }

    public void Enable(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public void Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Disable(string key)
    {
        throw new NotImplementedException();
    }

    public void Disable(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string key)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> CreateAsync(FlagCreationRequest contract, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> GetRequiredAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> GetRequiredAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse?> GetAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> GetRequiredAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse?> GetAsync(string name, string? parentKey, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> GetRequiredAsync(string name, string? parentKey, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, string? parentKey, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> UpdateAsync(Guid id, FlagUpdateRequest contract, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> UpdateAsync(string key, FlagUpdateRequest contract, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FlagResponse> DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DisableAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DisableAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DisableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
