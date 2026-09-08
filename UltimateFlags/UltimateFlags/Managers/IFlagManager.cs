using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Managers;

public interface IFlagManager
{
    public Flag Create(Flag entity);

    public Flag? Read(Guid id, bool? deleted = false);

    public Flag? Read(string key);

    public Flag? Read(string name, Guid? parentId);

    public IPagedList<Flag> List(
        string? searchString,
        bool? isOn,
        int pageNumber,
        int pageSize);

    public Flag Update(Guid id, FlagUpdateRequest contract);

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract);

    public Flag Delete(Guid id);

    public int ExecuteDelete(Guid id);

    public Flag Purge(Guid id);

    public int ExecutePurge(Guid id);

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null);

    public bool Exists(string name, Guid? parentId, bool? deleted = false);

    public void Enable(Guid id);

    public void Enable(string name, Guid? parentId);

    public void Disable(Guid id);

    public void Disable(string name, Guid? parentId);

    public bool IsOn(Guid id);

    public bool IsOn(string key);

    public int SaveChanges();
}
