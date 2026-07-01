using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Abstraction.Storages;

public interface IFlagStorage
{
    #region sync

    public Flag Create(Flag flag);

    public Flag? Read(string key);

    public Flag? Get(string key);

    public IEnumerable<Flag> List();

    public Flag Update(Flag flag);

    public Flag Delete(Flag flag);

    public void Enable(string key);

    public bool IsOn(string key);

    public int SaveChanges();

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(Flag flag, CancellationToken cancellationToken = default);

    public Task<Flag?> ReadAsync(string key, CancellationToken cancellationToken = default);

    public Task<Flag?> GetAsync(string key, CancellationToken cancellationToken = default);

    public Task<IEnumerable<Flag>> ListAsync(CancellationToken cancellationToken = default);

    public Task<Flag> UpdateAsync(Flag flag, CancellationToken cancellationToken = default);

    public Task<Flag> DeleteAsync(Flag flag, CancellationToken cancellationToken = default);

    public Task EnableAsync(string key, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion async
}
