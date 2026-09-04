using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Managers;

public interface IFlagManager
{
    public Flag Create(Flag entity);

    public Flag? Read(Guid id);

    public Flag? Read(string key);

    public Flag? Read(string name, Guid? parentId);

    public Flag? Get(Guid id);

    public Flag? Get(string key);

    public Flag? Get(string name, Guid? parentId);

    public IPagedList<Flag> List(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null);

    public IPagedList<Flag> Search(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null);

    public Flag Update(Guid id, FlagUpdateRequest contract);

    public Flag Delete(Guid id);

    public int Purge(Guid id);

    public int Purge(DateTime? fromInclusive = null, DateTime? toInclusive = null);

    public void Enable(Guid id);

    public void Enable(string name, Guid? parentId);

    public void Disable(Guid id);

    public void Disable(string name, Guid? parentId);

    public bool IsOn(Guid id);

    public bool IsOn(string key);

    public int SaveChanges();
}
