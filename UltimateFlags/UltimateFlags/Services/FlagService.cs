using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.ServerFaults;
using UltimateFlags.Abstraction.Services;
using UltimateFlags.Converters;
using UltimateFlags.Managers;
using UltimatePagination;
using UltimatePagination.Abstraction;

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
        Flag? existingFlag = _flagManager.Read(contract.Name, contract.ParentId);
        if (existingFlag is not null)
        {
            throw new FlagDuplicateFound { Area = $"{nameof(FlagService)}.{nameof(Create)}(contract)", };
        }

        Flag createdEntity = _flagManager.Create(contract.ToEntity());

        return _flagManager.SaveChanges() > 0
            ? createdEntity.ToContract()
            : throw new FlagCreationFailed { Area = $"{nameof(FlagService)}.{nameof(Create)}(contract)", };
    }

    public FlagResponse? Get(Guid id)
    {
        return _flagManager.Read(id)?.ToContract();
    }

    public FlagResponse GetRequired(Guid id)
    {
        Flag foundEntity =
            _flagManager.Read(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(id)",
            };

        return foundEntity.ToContract();
    }

    public FlagResponse? Get(string name, Guid? parentId)
    {
        return _flagManager.Read(name, parentId)?.ToContract();
    }

    public FlagResponse GetRequired(string name, Guid? parentId)
    {
        Flag foundEntity =
            _flagManager.Read(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(name, parentId)",
            };

        return foundEntity.ToContract();
    }

    public FlagResponse? Get(string key)
    {
        return _flagManager.Read(key)?.ToContract();
    }

    public FlagResponse GetRequired(string key)
    {
        Flag foundEntity =
            _flagManager.Read(key)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagService)}.{nameof(GetRequired)}(key)",
            };

        return foundEntity.ToContract();
    }

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new InvalidPaginationInfo
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, parentId, recursive, isOn, pageNumber, pageSize)",
            };
        }

        IPagedList<Flag> foundEntities =
            _flagManager
                .List(
                    searchString,
                    isOn,
                    pageNumber,
                    pageSize);

        return foundEntities.Convert(entity => entity.ToContract());
    }

    public IPagedList<FlagResponse> Search(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new InvalidPaginationInfo
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, parentId, recursive, isOn, pageNumber, pageSize)",
            };
        }

        IPagedList<Flag> foundEntities =
            _flagManager
                .Search(
                    searchString,
                    isOn,
                    pageNumber,
                    pageSize);

        return foundEntities.Convert(entity => entity.ToContract());
    }

    public FlagResponse Update(Guid id, FlagUpdateRequest contract)
    {
        Flag updatedEntity = _flagManager.Update(id, contract);

        return _flagManager.SaveChanges() > 0
            ? updatedEntity.ToContract()
            : throw new FlagUpdateFailed { Area = $"{nameof(FlagService)}.{nameof(Update)}(id, contract)", };
    }

    public FlagResponse Delete(Guid id)
    {
        Flag deleteEntity = _flagManager.Delete(id);

        return _flagManager.SaveChanges() > 0
            ? deleteEntity.ToContract()
            : throw new FlagDeletionFailed { Area = $"{nameof(FlagService)}.{nameof(Delete)}(id)", };
    }

    public int Purge(Guid id)
    {
        int purgedCount = _flagManager.Purge(id);
        if (purgedCount != 1)
        {
            throw new FlagPurgeFailed
            {
                Area = $"{nameof(FlagManager)}.{nameof(Purge)}(id)",
            };
        }

        return purgedCount;
    }

    public int Purge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        _validateTimeRange();

        return _flagManager.Purge(fromInclusive, toInclusive);

        void _validateTimeRange()
        {
            if (fromInclusive is null || toInclusive is null)
                return;

            if (fromInclusive.Value > toInclusive.Value)
            {
                throw new InvalidTimeRange
                {
                    Area = $"{nameof(FlagService)}.{nameof(Purge)}(from, to)",
                };
            }
        }
    }

    public void Enable(Guid id)
    {
        _flagManager.Enable(id);
    }

    public void Enable(string name, Guid? parentId)
    {
        _flagManager.Enable(name, parentId);
    }

    public void Disable(Guid id)
    {
        _flagManager.Disable(id);
    }

    public void Disable(string name, Guid? parentId)
    {
        _flagManager.Disable(name, parentId);
    }

    public bool IsOn(string key)
    {
        return _flagManager.IsOn(key);
    }

    public bool IsOn(Guid id)
    {
        return _flagManager.IsOn(id);
    }

    private static bool _IsValidPaginationInfo(int? pageNumber, int? pageSize)
    {
        return pageNumber is not null && pageSize is not null
            || pageNumber is null && pageSize is null;
    }
}
