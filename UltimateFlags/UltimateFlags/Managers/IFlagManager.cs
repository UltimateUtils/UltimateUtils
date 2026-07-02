using UltimateFlags.Abstraction.Entities;

namespace UltimateFlags.Managers;

public interface IFlagManager
{
    #region sync

    public Flag Create(string name, Guid? parentId, bool isOn);

    public Flag? Get(string name, Guid? parentId);

    public int SaveChanges();

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(
        string name,
        Guid? parentId,
        bool isOn,
        CancellationToken cancellationToken = default);

    public Task<Flag?> GetAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken = default);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion async
}
