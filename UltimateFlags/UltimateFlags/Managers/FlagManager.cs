using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.ServerFaults;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.Helpers;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Managers;

internal class FlagManager : IFlagManager
{
    private readonly ILogger<FlagManager> _logger;

    private readonly IFlagQueryStorage _flagQueryStorage;

    private readonly IFlagCommandStorage _flagCommandStorage;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagManager(
        ILogger<FlagManager> logger,
        IFlagQueryStorage flagQueryStorage,
        IFlagCommandStorage flagCommandStorage,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _flagQueryStorage = flagQueryStorage;
        _flagCommandStorage = flagCommandStorage;
        _ultimateFlagConfiguration = options.Value;
    }

    public Flag Create(Flag entity)
    {
        return _flagCommandStorage.Create(entity);
    }

    public Flag? Read(Guid id, bool? deleted = false)
    {
        return _flagQueryStorage.Read(id, deleted);
    }

    public Flag? Read(string key)
    {
        string[] names = key.Split('.');

        foreach (string name in names)
        {
            // todo - wip
        }

        // todo - KEY
        throw new NotImplementedException();
    }

    public Flag? Read(string name, Guid? parentId)
    {
        return _flagQueryStorage.Read(name, parentId);
    }

    public IPagedList<Flag> List(
        string? searchString,
        bool? isOn,
        int pageNumber,
        int pageSize)
    {
        return
            _flagQueryStorage
                .List(
                    searchString,
                    isOn,
                    pageNumber,
                    pageSize);
    }

    public Flag Update(Guid id, FlagUpdateRequest contract)
    {
        Flag entity =
            _flagCommandStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Update)}(id, contract)",
            };

        return _flagCommandStorage.Update(entity.UpdateFrom(contract));
    }

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract)
    {
        if (!_flagQueryStorage.Exists(id))
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(ExecuteUpdate)}(id, contract)",
            };
        }

        int updatedCount = _flagCommandStorage.ExecuteUpdate(id, contract);
        if (updatedCount == 1)
            return updatedCount;

        throw new FlagUpdateFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(ExecuteUpdate)}(id, contract)",
        };
    }

    public Flag Delete(Guid id)
    {
        Flag entity =
            _flagCommandStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Delete)}(id)",
            };

        return _flagCommandStorage.Delete(entity.Deleted());
    }

    public int ExecuteDelete(Guid id)
    {
        if (!_flagQueryStorage.Exists(id))
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(ExecuteDelete)}(id)",
            };
        }

        return _flagCommandStorage.ExecuteDelete(id);
    }

    public Flag Purge(Guid id)
    {
        Flag? flag = _flagCommandStorage.Get(id, deleted: true);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
            };
        }

        return _flagCommandStorage.Purge(flag);
    }

    public int ExecutePurge(Guid id)
    {
        if (!_flagQueryStorage.Exists(id, deleted: null))
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
            };
        }

        int purgedCount = _flagCommandStorage.ExecutePurge(id);

        if (purgedCount == 1)
            return purgedCount;

        throw new FlagNotDeleted
        {
            Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
        };
    }

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        return _flagCommandStorage.ExecutePurge(fromInclusive, toInclusive);
    }

    public bool Exists(
        string name,
        Guid? parentId,
        bool? deleted = false)
    {
        return _flagQueryStorage.Exists(name, parentId, deleted);
    }

    public void Enable(Guid id)
    {
        int enabledCount = _flagCommandStorage.Enable(id);

        if (enabledCount == 1)
            return;

        Flag? flag = _flagQueryStorage.Read(id);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Enable)}(id)",
            };
        }

        throw new FlagUpdateFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(Enable)}(id)",
        };
    }

    public void Enable(string name, Guid? parentId)
    {
        int enabledCount = _flagCommandStorage.Enable(name, parentId);

        if (enabledCount == 1)
            return;

        Flag? flag = _flagQueryStorage.Read(name, parentId);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Enable)}(name, parentId)",
            };
        }

        throw new FlagUpdateFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(Enable)}(name, parentId)",
        };
    }

    public void Disable(Guid id)
    {
        int disabledCount = _flagCommandStorage.Disable(id);

        if (disabledCount == 1)
            return;

        Flag? flag = _flagQueryStorage.Read(id);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Disable)}(id)",
            };
        }

        throw new FlagUpdateFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(Disable)}(id)",
        };
    }

    public void Disable(string name, Guid? parentId)
    {
        int disabledCount = _flagCommandStorage.Disable(name, parentId);

        if (disabledCount == 1)
            return;

        Flag? flag = _flagQueryStorage.Read(name, parentId);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Disable)}(name, parentId)",
            };
        }

        throw new FlagUpdateFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(Disable)}(name, parentId)",
        };
    }

    public bool IsOn(Guid id)
    {
        // todo - improve

        Flag entity =
            _flagQueryStorage.Read(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(id)",
            };

        return entity.IsOn;
    }

    public bool IsOn(string key)
    {
        // todo - KEY
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        // todo - improve

        Flag entity =
            _flagQueryStorage.Read(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(name, parentId)",
            };

        return entity.IsOn;
    }

    public int SaveChanges()
    {
        return _flagCommandStorage.SaveChanges();
    }
}
