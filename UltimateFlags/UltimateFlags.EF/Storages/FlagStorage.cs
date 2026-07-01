using Microsoft.Extensions.Logging;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.EF.Db;

namespace UltimateFlags.EF.Storages;

internal class FlagStorage : IFlagStorage
{
    private readonly ILogger<FlagStorage> _logger;

    private readonly FlagDbContext _flagDbContext;

    public FlagStorage(ILogger<FlagStorage> logger, FlagDbContext flagDbContext)
    {
        _logger = logger;
        _flagDbContext = flagDbContext;
    }

    #region sync

    public Flag Create(Flag flag)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(string key)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string key)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flag> List()
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

    public void Enable(string key)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string key)
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

    public Task<Flag?> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Flag>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> UpdateAsync(Flag flag, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> DeleteAsync(Flag flag, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnableAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion async
}
