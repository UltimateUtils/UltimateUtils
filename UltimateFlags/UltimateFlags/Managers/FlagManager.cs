using Microsoft.Extensions.Logging;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;

namespace UltimateFlags.Managers;

internal class FlagManager : IFlagManager
{
    private readonly IFlagStorage _storage;

    public FlagManager(
        ILogger<FlagManager> logger,
        IFlagStorage storage)
    {
        _storage = storage;
    }

    #region sync

    public Flag Create(Flag entity)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(string key)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flag> List()
    {
        throw new NotImplementedException();
    }

    public Flag Update(FlagUpdateRequest contract)
    {
        throw new NotImplementedException();
    }

    public Flag Delete(string key)
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

    public Task<Flag> CreateAsync(Flag entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> ReadAsync(string key, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Flag>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> UpdateAsync(FlagUpdateRequest contract, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> DeleteAsync(string key, CancellationToken cancellationToken = default)
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
