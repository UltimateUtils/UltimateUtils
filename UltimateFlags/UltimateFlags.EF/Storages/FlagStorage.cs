using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ServerFaults;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.EF.Db;
using UltimatePagination;
using UltimatePagination.Abstraction;
using UltimateUtils.Extensions;

namespace UltimateFlags.EF.Storages;

internal class FlagStorage : IFlagStorage
{
    private readonly ILogger<FlagStorage> _logger;

    private readonly IFlagDbContext _flagDbContext;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagStorage(
        ILogger<FlagStorage> logger,
        IFlagDbContext flagDbContext,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _flagDbContext = flagDbContext;
        _ultimateFlagConfiguration = options.Value;
    }

    public Flag Create(Flag flag)
    {
        EntityEntry<Flag> created = _flagDbContext.Flags.Add(flag);

        return created.Entity;
    }

    public Flag? Read(Guid id)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTracking()
                .FirstOrDefault(flag => flag.Id == id);
    }

    public Flag? Read(string name, Guid? parentId)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTracking()
                .FirstOrDefault(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId);
    }

    public Flag? Get(Guid id)
    {
        return _flagDbContext.Flags.Find(id);
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

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        bool? isOn, // todo
        int pageNumber,
        int pageSize)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTracking()
                .Where(
                    flag =>
                        flag.ParentId == parentId
                        && (searchString.IsNullOrEmpty()
                            || flag.Name.Contains(searchString)))
                .OrderBy(flag => flag.Name)
                .Paginate(pageNumber, pageSize);
    }

    public Flag Update(Flag flag)
    {
        EntityEntry<Flag> updated = _flagDbContext.Flags.Update(flag);

        return updated.Entity;
    }

    public Flag Delete(Flag flag)
    {
        EntityEntry<Flag> deleted = _flagDbContext.Flags.Remove(flag);

        return deleted.Entity;
    }

    public int Purge(Guid id)
    {
        return
            _flagDbContext
                .Flags
                .Where(f => f.Id == id && f.DeletedAt != null)
                .ExecuteDelete();
    }

    public int Purge(DateTime? fromInclusive, DateTime? toInclusive)
    {
        return
            _flagDbContext
                .Flags
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

    public bool IsOn(Guid id)
    {
        return
            _flagDbContext
                .Flags
                .Where(flag => flag.Id == id)
                .Select(flag => flag.IsOn)
                .FirstOrDefault();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        return
            _flagDbContext
                .Flags
                .Where(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId)
                .Select(flag => flag.IsOn)
                .FirstOrDefault();
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
