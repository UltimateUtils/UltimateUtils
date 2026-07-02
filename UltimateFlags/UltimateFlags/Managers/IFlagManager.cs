using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Managers;

public interface IFlagManager
{
    #region sync

    public Flag Create(string name, Guid? parentId, bool isOn);

    public Flag? Get(Guid id);

    public Flag? Get(string key);

    public Flag? Get(string name, Guid? parentId);

    public IPagedList<Flag> List(
        string? searchString,
        bool? isOn,
        int? pageNumber,
        int? pageSize);

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        bool? isOn,
        int? pageNumber,
        int? pageSize);

    public IPagedList<Flag> List(
        string? searchString,
        string? parentKey,
        bool? isOn,
        int? pageNumber,
        int? pageSize);

    public Flag Update(Guid id, FlagUpdateRequest contract);

    public Flag Update(string key, FlagUpdateRequest contract);

    public Flag Delete(Guid id);

    public Flag Delete(string key);

    public void Enable(Guid id);

    public void Enable(string key);

    public void Enable(string name, Guid? parentId);

    public void Disable(Guid id);

    public void Disable(string key);

    public void Disable(string name, Guid? parentId);

    public bool IsOn(Guid id);

    public bool IsOn(string key);

    public bool IsOn(string name, Guid? parentId);

    public int SaveChanges();

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(
        string name,
        Guid? parentId,
        bool isOn,
        CancellationToken cancellationToken = default);

    public Task<Flag?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<Flag?> GetAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken = default);

    public Task<Flag> DeleteAsync(
        Guid id,
        CancellationToken cancellation = default);

    public Task<Flag> DeleteAsync(
        string key,
        CancellationToken cancellation = default);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion async
}
