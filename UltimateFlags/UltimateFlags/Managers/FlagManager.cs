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

    private readonly IFlagStorage _flagStorage;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagManager(
        ILogger<FlagManager> logger,
        IFlagStorage flagStorage,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _flagStorage = flagStorage;
        _ultimateFlagConfiguration = options.Value;
    }

    public Flag Create(Flag entity)
    {
        return _flagStorage.Create(entity);
    }

    public Flag? Read(Guid id)
    {
        return _flagStorage.Read(id);
    }

    public Flag? Read(string key)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(string name, Guid? parentId)
    {
        return _flagStorage.Read(name, parentId);
    }

    public Flag? Get(Guid id)
    {
        return _flagStorage.Get(id);
    }

    public Flag? Get(string key)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string name, Guid? parentId)
    {
        return _flagStorage.Get(name, parentId);
    }

    public IPagedList<Flag> List(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> Search(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null)
    {
        throw new NotImplementedException();
    }

    public Flag Update(Guid id, FlagUpdateRequest contract)
    {
        Flag entity =
            _flagStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Update)}(id, contract)",
            };

        return _flagStorage.Update(entity.UpdateFrom(contract));
    }

    public Flag Delete(Guid id)
    {
        Flag entity =
            _flagStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Delete)}(id)",
            };

        return _flagStorage.Delete(entity);
    }

    public int Purge(Guid id)
    {
        Flag? flag = _flagStorage.Get(id);

        if (flag is null)
        {
            throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Purge)}(id)",
            };
        }

        if (flag.DeletedAt is null)
        {
            throw new FlagNotDeleted
            {
                Area = $"{nameof(FlagManager)}.{nameof(Purge)}(id)",
            };
        }

        int purgedCount = _flagStorage.Purge(id);
        if (purgedCount == 1)
            return purgedCount;

        throw new FlagPurgeFailed
        {
            Area = $"{nameof(FlagManager)}.{nameof(Purge)}(id)",
        };
    }

    public int Purge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        return _flagStorage.Purge(fromInclusive, toInclusive);
    }

    public void Enable(Guid id)
    {
        int enabledCount = _flagStorage.Enable(id);

        if (enabledCount == 1)
            return;

        Flag? flag = _flagStorage.Read(id);

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
        int enabledCount = _flagStorage.Enable(name, parentId);

        if (enabledCount == 1)
            return;

        Flag? flag = _flagStorage.Read(name, parentId);

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
        int disabledCount = _flagStorage.Disable(id);

        if (disabledCount == 1)
            return;

        Flag? flag = _flagStorage.Read(id);

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
        int disabledCount = _flagStorage.Disable(name, parentId);

        if (disabledCount == 1)
            return;

        Flag? flag = _flagStorage.Read(name, parentId);

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
        Flag entity =
            _flagStorage.Read(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(id)",
            };

        return entity.IsOn;
    }

    public bool IsOn(string key)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        Flag entity =
            _flagStorage.Read(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(name, parentId)",
            };

        return entity.IsOn;
    }

    public int SaveChanges()
    {
        return _flagStorage.SaveChanges();
    }
}
