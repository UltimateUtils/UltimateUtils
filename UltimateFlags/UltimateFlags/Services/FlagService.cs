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
        if (contract.ParentId is not null)
        {
            // todo - improve with projection
            Flag? parentFlag = _flagManager.Read(contract.ParentId.Value);

            if (parentFlag is null)
            {
                throw new FlagParentNotFound
                {
                    Area = $"{nameof(FlagService)}.{nameof(Create)}(contract)",
                };
            }

            // todo - improve readability with extension method
            if (parentFlag.DeletedAt is not null)
            {
                throw new FlagParentDeleted
                {
                    Area = $"{nameof(FlagService)}.{nameof(Create)}(contract)",
                };
            }
        }

        bool exists = _flagManager.Exists(contract.Name, contract.ParentId, deleted: null);
        if (exists)
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
        int pageNumber = 1,
        int pageSize = 20)
    {
        if (!_IsValidPaginationInfo(pageNumber, pageSize))
        {
            throw new InvalidPaginationInfo
            {
                Area = $"{nameof(FlagService)}.{nameof(List)}(searchString, isOn, pageNumber, pageSize)",
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

    public FlagResponse Update(Guid id, FlagUpdateRequest contract)
    {
        Flag updatedEntity = _flagManager.Update(id, contract);

        return
            _flagManager.SaveChanges() > 0
                ? updatedEntity.ToContract()
                : throw new FlagUpdateFailed { Area = $"{nameof(FlagService)}.{nameof(Update)}(id, contract)", };
    }

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract)
    {
        return _flagManager.ExecuteUpdate(id, contract);
    }

    public FlagResponse Delete(Guid id)
    {
        // todo - cascade delete through hierarchy

        Flag deleteEntity = _flagManager.Delete(id);

        return
            _flagManager.SaveChanges() > 0
                ? deleteEntity.ToContract()
                : throw new FlagDeletionFailed { Area = $"{nameof(FlagService)}.{nameof(Delete)}(id)", };
    }

    public int ExecuteDelete(Guid id)
    {
        // todo - cascade delete through hierarchy

        return _flagManager.ExecuteDelete(id);
    }

    public FlagResponse Purge(Guid id)
    {
        // todo - cascade delete through hierarchy

        Flag purgedEntity = _flagManager.Purge(id);

        return
            _flagManager.SaveChanges() > 0
                ? purgedEntity.ToContract()
                : throw new FlagPurgeFailed { Area = $"{nameof(FlagService)}.{nameof(Purge)}(id)", };
    }

    public int ExecutePurge(Guid id)
    {
        // todo - cascade delete through hierarchy

        return _flagManager.ExecutePurge(id);
    }

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        // todo - cascade delete through hierarchy

        _validateTimeRange();

        return _flagManager.ExecutePurge(fromInclusive, toInclusive);

        void _validateTimeRange()
        {
            if (fromInclusive is null || toInclusive is null)
                return;

            if (fromInclusive.Value > toInclusive.Value)
            {
                throw new InvalidTimeRange
                {
                    Area = $"{nameof(FlagService)}.{nameof(ExecutePurge)}(from, to)",
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

    private static bool _IsValidPaginationInfo(int pageNumber, int pageSize)
    {
        return pageNumber > 0 && pageSize > 0;
    }
}
