using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.ServerFaults;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.Helpers;
using UltimateFlags.Utils;
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
        string[] names = key.Split(Constants.KeyDelimiter);

        Guid? parentId = null;
        Flag? currentFlag = null;

        foreach (string name in names)
        {
            // todo - improve - projection
            currentFlag = _flagQueryStorage.Read(name, parentId);

            if (currentFlag is null)
                return null;

            parentId = currentFlag.Id;
        }

        return currentFlag;
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

    public IEnumerable<Flag> Delete(Guid id)
    {
        // todo - improve
        Flag flagToDelete =
            _flagQueryStorage.Read(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Delete)}(id)",
            };

        List<Flag> deletedFlags = [];
        Queue<Flag> flagsToDelete = new();

        flagsToDelete.Enqueue(flagToDelete);

        while (flagsToDelete.Count > 0)
        {
            Flag curr = flagsToDelete.Dequeue();
            Flag deleted = _flagCommandStorage.Delete(curr);

            deletedFlags.Add(deleted);

            IEnumerable<Flag> nextGeneration = _flagQueryStorage.ReadAll(parentId: deleted.Id);

            // todo - improve with PushRange()
            foreach (Flag flag in nextGeneration)
            {
                flagsToDelete.Enqueue(flag);
            }
        }

        return deletedFlags;
    }

    public int ExecuteDelete(Guid id)
    {
        IEnumerable<Guid> idsToDelete = _getAllDescendantIds(id);
        return _flagCommandStorage.ExecuteDelete(idsToDelete);

        IEnumerable<Guid> _getAllDescendantIds(Guid rootId)
        {
            if (!_flagQueryStorage.Exists(rootId))
            {
                throw new FlagNotFound
                {
                    Area = $"{nameof(FlagManager)}.{nameof(Delete)}(id)",
                };
            }

            yield return rootId;

            List<Guid> frontier = [rootId];
            while (frontier.Count > 0)
            {
                List<Guid> nextGeneration = [];

                foreach (Guid parentId in frontier)
                {
                    Guid[] ids = [.. _flagQueryStorage.ReadAll(parentId).Select(f => f.Id)];
                    nextGeneration.AddRange(ids);

                    foreach (Guid guid in ids)
                    {
                        yield return guid;
                    }
                }

                frontier = nextGeneration;
            }
        }
    }

    public IEnumerable<Flag> Purge(Guid id)
    {
        // todo - cascade delete logic here

        // Flag? flag = _flagCommandStorage.Get(id, deleted: true);
        //
        // if (flag is null)
        // {
        //     throw new FlagNotFound
        //     {
        //         Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
        //     };
        // }
        //
        // return _flagCommandStorage.Purge(flag);

        throw new NotImplementedException();
    }

    public int ExecutePurge(Guid id)
    {
        // todo - cascade delete logic here

        // if (!_flagQueryStorage.Exists(id, deleted: null))
        // {
        //     throw new FlagNotFound
        //     {
        //         Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
        //     };
        // }
        //
        // int purgedCount = _flagCommandStorage.ExecutePurge(id);
        //
        // if (purgedCount == 1)
        //     return purgedCount;
        //
        // throw new FlagNotDeleted
        // {
        //     Area = $"{nameof(FlagManager)}.{nameof(ExecutePurge)}(id)",
        // };

        throw new NotImplementedException();
    }

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        // todo - cascade delete logic here

        // return _flagCommandStorage.ExecutePurge(fromInclusive, toInclusive);

        throw new NotImplementedException();
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
        // todo - improve - projection

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
        string[] names = key.Split(Constants.KeyDelimiter);

        Guid? parentId = null;
        Flag? currentFlag = null;

        foreach (string name in names)
        {
            // todo - improve - projection
            currentFlag = _flagQueryStorage.Read(name, parentId);
            if (currentFlag is null || !currentFlag.IsOn)
                return false;

            parentId = currentFlag.Id;
        }

        return currentFlag?.IsOn ?? false;
    }

    public bool IsOn(string name, Guid? parentId)
    {
        // todo - improve - projection

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
