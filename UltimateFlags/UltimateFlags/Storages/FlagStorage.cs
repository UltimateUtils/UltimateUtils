using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Storages;

internal class FlagStorage : IFlagStorage
{
    private readonly ILogger<FlagStorage> _logger;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagStorage(
        ILogger<FlagStorage> logger,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _ultimateFlagConfiguration = options.Value;
    }

    #region sync

    public Flag Create(Flag flag)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(Guid id)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> List(
        string? searchString,
        int pageNumber,
        int pageSize)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        int pageNumber,
        int pageSize)
    {
        throw new NotImplementedException();
    }

    public Flag Update(Flag flag)
    {
        throw new NotImplementedException();
    }

    public Flag Delete(Flag flag)
    {
        throw new NotImplementedException();
    }

    public Flag Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Enable(Guid id)
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

    public void Disable(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(Flag flag, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> ReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> ReadAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Flag?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> GetAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IPagedList<Flag>> ListAsync(
        string? searchString,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IPagedList<Flag>> ListAsync(
        string? searchString,
        Guid? parentId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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

    public Task EnableAsync(Guid id, CancellationToken cancellationToken = default)
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

    public Task DisableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(string name, Guid? parentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion async
}
