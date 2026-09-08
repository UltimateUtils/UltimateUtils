using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.EF.Db;

namespace UltimateFlags.EF.Storages;

public class FlagCommandStorage : IFlagCommandStorage
{
    private readonly ILogger<FlagCommandStorage> _logger;

    private readonly IFlagDbContext _flagDbContext;

    private readonly IQueryable<Flag> _flags;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagCommandStorage(
        ILogger<FlagCommandStorage> logger,
        IFlagDbContext flagDbContext,
        UltimateFlagConfiguration ultimateFlagConfiguration)
    {
        _logger = logger;
        _flagDbContext = flagDbContext;
        _flags = flagDbContext.Flags;
        _ultimateFlagConfiguration = ultimateFlagConfiguration;
    }

    public Flag? Get(Guid id, bool? deleted = false)
    {
        return
            deleted is null
                ? _flagDbContext.Flags.IgnoreQueryFilters().FirstOrDefault(f => f.Id == id)
                : deleted.Value
                    ? _flagDbContext
                        .Flags
                        .IgnoreQueryFilters()
                        .FirstOrDefault(
                            flag =>
                                flag.Id == id
                                && flag.DeletedAt.HasValue)
                    : _flagDbContext.Flags.Find(id);
    }

    public Flag? Get(string name, Guid? parentId)
    {
        return
            _flagDbContext
                .Flags
                .FirstOrDefault(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId);
    }

    public Flag Create(Flag flag)
    {
        EntityEntry<Flag> created = _flagDbContext.Flags.Add(flag);

        return created.Entity;
    }

    public Flag Update(Flag flag)
    {
        EntityEntry<Flag> updated = _flagDbContext.Flags.Update(flag);

        return updated.Entity;
    }

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTracking() // todo
                .Where(f => f.Id == id)
                .ExecuteUpdate(
                    setters =>
                        setters
                            .SetProperty(f => f.Name, f => contract.Name ?? f.Name)
                            .SetProperty(f => f.Description, f => contract.Description ?? f.Description) // todo - null로 업데이트하고 싶다면?
                            .SetProperty(f => f.IsOn, f => contract.IsOn ?? f.IsOn));
    }

    public Flag Delete(Flag flag)
    {
        EntityEntry<Flag> softDeleted =
            _flagDbContext
                .Flags
                .Update(flag);

        return softDeleted.Entity;
    }

    public int ExecuteDelete(Guid id)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTracking() // todo
                .Where(f => f.Id == id)
                .ExecuteDelete();
    }

    public Flag Purge(Flag flag)
    {
        EntityEntry<Flag> hardDeleted = _flagDbContext.Flags.Remove(flag);

        return hardDeleted.Entity;
    }

    public int ExecutePurge(Guid id)
    {
        // todo - hierarchy
        return
            _flagDbContext
                .Flags
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(f => f.Id == id && f.DeletedAt.HasValue)
                .ExecuteDelete();
    }

    public int ExecutePurge(DateTime? fromInclusive, DateTime? toInclusive)
    {
        // todo - hierarchy
        return
            _flagDbContext
                .Flags
                .IgnoreQueryFilters()
                .Where(
                    f =>
                        f.DeletedAt.HasValue
                        && (fromInclusive == null || f.DeletedAt.Value >= fromInclusive.Value)
                        && (toInclusive == null || f.DeletedAt.Value <= toInclusive.Value))
                .ExecuteDelete();
    }

    public int Enable(Guid id)
    {
        // todo - 원래 켜져 있는 경우 return 값이 어떻게 되는지 확인
        return
            _flagDbContext
                .Flags
                .Where(f => f.Id == id)
                .ExecuteUpdate(
                    setters =>
                        setters
                            .SetProperty(
                                f => f.IsOn,
                                true));
    }

    public int Enable(string name, Guid? parentId)
    {
        // todo - 원래 켜져 있는 경우 return 값이 어떻게 되는지 확인
        return
            _flagDbContext
                .Flags
                .Where(f => f.Name == name && f.ParentId == parentId)
                .ExecuteUpdate(
                    setters =>
                        setters
                            .SetProperty(
                                f => f.IsOn,
                                true));
    }

    public int Disable(Guid id)
    {
        // todo - 원래 켜져 있는 경우 return 값이 어떻게 되는지 확인
        return
            _flagDbContext
                .Flags
                .Where(f => f.Id == id)
                .ExecuteUpdate(
                    setters =>
                        setters
                            .SetProperty(
                                f => f.IsOn,
                                false));
    }

    public int Disable(string name, Guid? parentId)
    {
        // todo - 원래 켜져 있는 경우 return 값이 어떻게 되는지 확인
        return
            _flagDbContext
                .Flags
                .Where(f => f.Name == name && f.ParentId == parentId)
                .ExecuteUpdate(
                    setters =>
                        setters
                            .SetProperty(
                                f => f.IsOn,
                                false));
    }

    public int SaveChanges()
    {
        try
        {
            return _flagDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "{ErrorMessage}", e.Message);

            return 0;
        }
    }
}
