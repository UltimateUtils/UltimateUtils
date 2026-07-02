using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Exceptions.Reasons;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.EF.Db;
using UltimatePagination;
using UltimatePagination.Abstraction;
using UltimatePagination.EF;
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

    #region sync

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
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(flag => flag.Id == id);
    }

    public Flag? Read(string name, Guid? parentId)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId);
    }

    public Flag? Get(Guid id)
    {
        Flag? found = _flagDbContext.Flags.Find(id);

        return found?.DeletedAt == null
            ? found
            : null;
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
        int pageNumber,
        int pageSize)
    {
        return
            _flagDbContext
                .Flags
                .Where(
                    flag =>
                        searchString.IsNullOrEmpty()
                        || flag.Name.Contains(searchString))
                .OrderBy(flag => flag.Name)
                .Paginate(pageNumber, pageSize);
    }

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
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

    public Flag Delete(Guid id)
    {
        Flag? flag = _flagDbContext.Flags.Find(id);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(Delete)}(Guid id)");

        EntityEntry<Flag> deleted = _flagDbContext.Flags.Remove(flag);

        return deleted.Entity;
    }

    public void Enable(Guid id)
    {
        Flag? flag = _flagDbContext.Flags.Find(id);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(Enable)}(Guid id)");

        if (flag.IsOn)
            return;

        flag.IsOn = true;
        _flagDbContext.Flags.Update(flag);
    }

    public void Enable(string name, Guid? parentId)
    {
        Flag? flag =
            _flagDbContext
                .Flags
                .FirstOrDefault(
                    flag =>
                        flag.ParentId == parentId
                        && (flag.Name == name || flag.IsOn));

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(Enable)}(string name, Guid? parentId)");

        if (flag.IsOn)
            return;

        flag.IsOn = true;

        _flagDbContext.Flags.Update(flag);
    }

    public void Disable(Guid id)
    {
        Flag? flag = _flagDbContext.Flags.Find(id);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(Disable)}(Guid id)");

        if (!flag.IsOn)
            return;

        flag.IsOn = false;
        _flagDbContext.Flags.Update(flag);
    }

    public void Disable(string name, Guid? parentId)
    {
        Flag? flag =
            _flagDbContext
                .Flags
                .FirstOrDefault(
                    flag =>
                        flag.ParentId == parentId
                        && (flag.Name == name || flag.IsOn));

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(Disable)}(string name, Guid? parentId)");

        if (!flag.IsOn)
            return;

        flag.IsOn = false;

        _flagDbContext.Flags.Update(flag);
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
        return _flagDbContext.SaveChanges();
    }

    #endregion sync

    #region async

    public async Task<Flag> CreateAsync(Flag flag, CancellationToken cancellationToken = default)
    {
        EntityEntry<Flag> created =
            await
                _flagDbContext
                    .Flags
                    .AddAsync(flag, cancellationToken)
                    .ConfigureAwait(false);

        return created.Entity;
    }

    public Task<Flag?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(
                    flag => flag.Id == id,
                    cancellationToken);
    }

    public Task<Flag?> ReadAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return
            _flagDbContext
                .Flags
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId,
                    cancellationToken);
    }

    public async ValueTask<Flag?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Flag? found = await _flagDbContext.Flags.FindAsync([id], cancellationToken);

        return
            found?.DeletedAt == null
                ? found
                : null;
    }

    public Task<Flag?> GetAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return
            _flagDbContext
                .Flags
                .FirstOrDefaultAsync(
                    flag =>
                        flag.Name == name
                        && flag.ParentId == parentId,
                    cancellationToken);
    }

    public Task<IPagedList<Flag>> ListAsync(
        string? searchString,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        return
            _flagDbContext
                .Flags
                .Where(
                    flag =>
                        searchString.IsNullOrEmpty()
                        || flag.Name.Contains(searchString))
                .OrderBy(flag => flag.Name)
                .PaginateAsync(pageNumber, pageSize, cancellationToken);
    }

    public Task<IPagedList<Flag>> ListAsync(
        string? searchString,
        Guid? parentId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
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
                .PaginateAsync(pageNumber, pageSize, cancellationToken);
    }

    // public Task<Flag> UpdateAsync(Flag flag, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Flag> DeleteAsync(Flag flag, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Flag> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task EnableAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Flag? flag =
            await
                _flagDbContext
                    .Flags
                    .FindAsync([id], cancellationToken)
                    .ConfigureAwait(false);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(EnableAsync)}(Guid id, CancellationToken cancellationToken)");

        if (flag.IsOn)
            return;

        flag.IsOn = true;

        _flagDbContext.Flags.Update(flag);
    }

    public async Task EnableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        Flag? flag =
            await
                _flagDbContext
                    .Flags
                    .FirstOrDefaultAsync(
                        flag =>
                            flag.ParentId == parentId
                            && (flag.Name == name || flag.IsOn),
                        cancellationToken)
                    .ConfigureAwait(false);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(EnableAsync)}(string name, Guid? parentId, CancellationToken cancellationToken)");

        if (flag.IsOn)
            return;

        flag.IsOn = true;

        _flagDbContext.Flags.Update(flag);
    }

    public async Task DisableAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Flag? flag =
            await
                _flagDbContext
                    .Flags
                    .FindAsync([id], cancellationToken)
                    .ConfigureAwait(false);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(DisableAsync)}(Guid id, CancellationToken cancellationToken)");

        if (!flag.IsOn)
            return;

        flag.IsOn = false;

        _flagDbContext.Flags.Update(flag);
    }

    public async Task DisableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        Flag? flag =
            await
                _flagDbContext
                    .Flags
                    .FirstOrDefaultAsync(
                        flag =>
                            flag.ParentId == parentId
                            && (flag.Name == name || flag.IsOn),
                        cancellationToken)
                    .ConfigureAwait(false);

        _EnsureExists(flag, $"{nameof(FlagStorage)}.{nameof(DisableAsync)}(string name, Guid? parentId, CancellationToken cancellationToken)");

        if (!flag.IsOn)
            return;

        flag.IsOn = false;

        _flagDbContext.Flags.Update(flag);
    }

    public async Task<bool> IsOnAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return
            await
                _flagDbContext
                    .Flags
                    .Where(flag => flag.Id == id)
                    .Select(flag => flag.IsOn)
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
    }

    public async Task<bool> IsOnAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        return
            await
                _flagDbContext
                    .Flags
                    .Where(
                        flag =>
                            flag.Name == name
                            && flag.ParentId == parentId)
                    .Select(flag => flag.IsOn)
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _flagDbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion async

    private static void _EnsureExists([NotNull] Flag? flag, string area)
    {
        if (flag is not { DeletedAt: null })
        {
            throw new FlagNotFound
            {
                Reason = ClientFaultReason.FlagNotFound,
                Area = area,
            };
        }
    }
}
