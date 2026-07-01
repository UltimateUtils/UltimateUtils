using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Managers;

public interface IFlagManager
{
    #region sync

    public Flag Create(Flag entity);

    public Flag? Read(string key);

    public IEnumerable<Flag> List();

    public Flag Update(FlagUpdateRequest contract);

    public Flag Delete(string key);

    public void Enable(string key);

    public bool IsOn(string key);

    public int SaveChanges();

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(Flag entity, CancellationToken cancellationToken = default);

    public Task<Flag?> ReadAsync(string key, CancellationToken cancellationToken = default);

    public Task<IEnumerable<Flag>> ListAsync(CancellationToken cancellationToken = default);

    public Task<Flag> UpdateAsync(FlagUpdateRequest contract, CancellationToken cancellationToken = default);

    public Task<Flag> DeleteAsync(string key, CancellationToken cancellationToken = default);

    public Task EnableAsync(string key, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion async
}
