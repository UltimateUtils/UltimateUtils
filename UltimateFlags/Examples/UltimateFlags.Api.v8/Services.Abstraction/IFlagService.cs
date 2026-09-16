using UltimateFlags.Abstraction.Contracts;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Api.v8.Services.Abstraction;

public interface IFlagService
{
    public FlagResponse Create(FlagCreationRequest contract);

    public FlagResponse? Get(Guid id);

    public FlagResponse GetRequired(Guid id);

    public FlagResponse? Get(string name, Guid? parentId);

    public FlagResponse GetRequired(string name, Guid? parentId);

    public FlagResponse? Get(string key);

    public FlagResponse GetRequired(string key);

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        bool? isOn = null,
        int pageNumber = 1,
        int pageSize = 20);

    public FlagResponse Update(Guid id, FlagUpdateRequest contract);

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract);

    public IEnumerable<FlagResponse> Delete(Guid id);

    public int ExecuteDelete(Guid id);

    public IEnumerable<FlagResponse> Purge(Guid id);

    public int ExecutePurge(Guid id);

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null);

    public void Enable(Guid id);

    public void Enable(string name, Guid? parentId);

    public void Disable(Guid id);

    public void Disable(string name, Guid? parentId);

    public bool IsOn(string key);

    public bool IsOn(Guid id);
}
