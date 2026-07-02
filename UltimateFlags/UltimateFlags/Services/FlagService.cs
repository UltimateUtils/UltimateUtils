using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.Reasons;
using UltimateFlags.Abstraction.Services;
using UltimateFlags.Converters;
using UltimateFlags.Managers;
using UltimatePagination;
using UltimatePagination.Abstraction;
using UltimateUtils.Extensions;

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
        if (_flagManager.Get(contract.Key) is not null)
        {
            throw new FlagDuplicateFound
            {
                Area = $"{nameof(FlagService)}.{nameof(Create)}(contract)",
            };
        }

        FlagResponse? flagResponse = _create(null, contract.Key, contract.IsOn);

        _flagManager.SaveChanges();

        return flagResponse ?? throw new InvalidOperationException();

        FlagResponse? _create(Guid? parentId, string key, bool isOn)
        {
            if (key.IsNullOrEmpty())
            {
                return null;
            }

            (string name, string remainingKey) = _parseKey(key);

            Flag curr =
                _flagManager.Get(name, parentId)
                ?? _createFlag(name, parentId, isOn);

            FlagResponse? remaining = _create(curr.Id, remainingKey, isOn);

            return remaining is null
                ? new FlagResponse
                {
                    Key = key,
                    IsOn = curr.IsOn,
                    CreatedAt = curr.CreatedAt,
                    UpdatedAt = curr.UpdatedAt,
                }
                : new FlagResponse
                {
                    Key = key,
                    IsOn = curr.IsOn && remaining.IsOn,
                    CreatedAt = remaining.CreatedAt,
                    UpdatedAt = remaining.UpdatedAt,
                };
        }

        (string name, string remainingKey) _parseKey(string key)
        {
            int firstDotIndex = key.IndexOf('.');

            return firstDotIndex < 0
                ? (key, string.Empty)
                : (key[..firstDotIndex], key[(firstDotIndex + 1)..]);
        }

        Flag _createFlag(string name, Guid? parentId, bool isOn)
        {
            Flag flag = _flagManager.Create(name, parentId, isOn);

            _flagManager.SaveChanges();

            return flag;
        }
    }

    public FlagResponse? Get(Guid id)
    {
        return _flagManager.Get(id)?.ToContract();
    }

    public FlagResponse GetRequired(Guid id)
    {
        Flag flag =
            _flagManager.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(id)",
            };

        return flag.ToContract();
    }

    public FlagResponse? Get(string key)
    {
        return _flagManager.Get(key)?.ToContract();
    }

    public FlagResponse GetRequired(string key)
    {
        Flag flag =
            _flagManager.Get(key)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(key)",
            };

        return flag.ToContract();
    }

    public FlagResponse? Get(string name, Guid? parentId)
    {
        return _flagManager.Get(name, parentId)?.ToContract();
    }

    public FlagResponse GetRequired(string name, Guid? parentId)
    {
        Flag flag =
            _flagManager.Get(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(name, parentId)",
            };

        return flag.ToContract();
    }

    public FlagResponse? Get(string name, string? parentKey)
    {
        string key =
            parentKey is null
                ? name
                : $"{name}.{parentKey}";

        return _flagManager.Get(key)?.ToContract();
    }

    public FlagResponse GetRequired(string name, string? parentKey)
    {
        string key =
            parentKey is null
                ? name
                : $"{name}.{parentKey}";

        Flag flag =
            _flagManager.Get(key)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(name, parentKey)",
            };

        return flag.ToContract();
    }

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new PaginationInfoInvalid
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, isOn, pageNumber, pageSize)",
            };
        }

        IPagedList<Flag> foundEntities = _flagManager.List(searchString, isOn, pageNumber, pageSize);

        return foundEntities.Convert(e => e.ToContract());
    }

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        Guid? parentId = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new PaginationInfoInvalid
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, parentId, isOn, pageNumber, pageSize)",
            };
        }

        IPagedList<Flag> foundEntities = _flagManager.List(searchString, parentId, isOn, pageNumber, pageSize);

        return foundEntities.Convert(e => e.ToContract());
    }

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        string? parentKey = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new PaginationInfoInvalid
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, parentKey, isOn, pageNumber, pageSize)",
            };
        }

        IPagedList<Flag> foundEntities = _flagManager.List(searchString, parentKey, isOn, pageNumber, pageSize);

        return foundEntities.Convert(e => e.ToContract());
    }

    public FlagResponse Update(Guid id, FlagUpdateRequest contract)
    {
        Flag updatedEntity = _flagManager.Update(id, contract);

        _flagManager.SaveChanges();

        return updatedEntity.ToContract();
    }

    public FlagResponse Update(string key, FlagUpdateRequest contract)
    {
        Flag updatedEntity = _flagManager.Update(key, contract);

        _flagManager.SaveChanges();

        return updatedEntity.ToContract();
    }

    public FlagResponse Delete(Guid id)
    {
        Flag delete = _flagManager.Delete(id);

        _flagManager.SaveChanges();

        return delete.ToContract();
    }

    public FlagResponse Delete(string key)
    {
        Flag delete = _flagManager.Delete(key);

        _flagManager.SaveChanges();

        return delete.ToContract();
    }

    public void Enable(Guid id)
    {
        _flagManager.Enable(id);
        _flagManager.SaveChanges();
    }

    public void Enable(string key)
    {
        _flagManager.Enable(key);
        _flagManager.SaveChanges();
    }

    public void Enable(string name, Guid? parentId)
    {
        _flagManager.Enable(name, parentId);
        _flagManager.SaveChanges();
    }

    public void Disable(Guid id)
    {
        _flagManager.Disable(id);
        _flagManager.SaveChanges();
    }

    public void Disable(string key)
    {
        _flagManager.Disable(key);
        _flagManager.SaveChanges();
    }

    public void Disable(string name, Guid? parentId)
    {
        _flagManager.Disable(name, parentId);
        _flagManager.SaveChanges();
    }

    public bool IsOn(Guid id)
    {
        return _flagManager.IsOn(id);
    }

    public bool IsOn(string key)
    {
        return _flagManager.IsOn(key);
    }

    public bool IsOn(string name, Guid? parentId)
    {
        return _flagManager.IsOn(name, parentId);
    }

    #endregion sync

    #region async

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

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString = null, bool? isOn = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString = null, Guid? parentId = null, bool? isOn = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString = null, string? parentKey = null, bool? isOn = null, CancellationToken cancellationToken = default)
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

    #endregion async

    private bool _IsValidPaginationInfo(int? pageNumber, int? pageSize)
    {
        return pageNumber is not null && pageSize is not null
            || pageNumber is null && pageSize is null;
    }
}
