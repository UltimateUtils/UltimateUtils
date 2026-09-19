using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Api.v9.Services.Abstraction;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Api.v9.Services;

public class FlagService : IFlagService
{
    private readonly ILogger<FlagService> _logger;

    private readonly UltimateFlags.Abstraction.Services.IFlagService _flagService;

    private readonly UltimateFlags.Abstraction.Services.IFlagManagementService _flagManagementService;

    public FlagService(
        ILogger<FlagService> logger,
        UltimateFlags.Abstraction.Services.IFlagService flagService,
        UltimateFlags.Abstraction.Services.IFlagManagementService flagManagementService)
    {
        _logger = logger;
        _flagService = flagService;
        _flagManagementService = flagManagementService;
    }

    public FlagResponse Create(FlagCreationRequest contract)
    {
        return _flagManagementService.Create(contract);
    }

    public FlagResponse? Get(Guid id)
    {
        return _flagManagementService.Get(id);
    }

    public FlagResponse GetRequired(Guid id)
    {
        return _flagManagementService.GetRequired(id);
    }

    public FlagResponse? Get(string name, Guid? parentId)
    {
        return _flagManagementService.Get(name, parentId);
    }

    public FlagResponse GetRequired(string name, Guid? parentId)
    {
        return _flagManagementService.GetRequired(name, parentId);
    }

    public FlagResponse? Get(string key)
    {
        return _flagManagementService.Get(key);
    }

    public FlagResponse GetRequired(string key)
    {
        return _flagManagementService.GetRequired(key);
    }

    public IPagedList<FlagResponse> List(
        string? searchString = null,
        bool? isOn = null,
        int pageNumber = 1,
        int pageSize = 20)
    {
        return _flagManagementService.List(
            searchString,
            isOn,
            pageNumber,
            pageSize);
    }

    public FlagResponse Update(Guid id, FlagUpdateRequest contract)
    {
        return _flagManagementService.Update(id, contract);
    }

    public int ExecuteUpdate(Guid id, FlagUpdateRequest contract)
    {
        return _flagManagementService.ExecuteUpdate(id, contract);
    }

    public IEnumerable<FlagResponse> Delete(Guid id)
    {
        return _flagManagementService.Delete(id);
    }

    public int ExecuteDelete(Guid id)
    {
        return _flagManagementService.ExecuteDelete(id);
    }

    public IEnumerable<FlagResponse> Purge(Guid id)
    {
        return _flagManagementService.Purge(id);
    }

    public int ExecutePurge(Guid id)
    {
        return _flagManagementService.ExecutePurge(id);
    }

    public int ExecutePurge(DateTime? fromInclusive = null, DateTime? toInclusive = null)
    {
        return _flagManagementService.ExecutePurge(fromInclusive, toInclusive);
    }

    public void Enable(Guid id)
    {
        _flagManagementService.Enable(id);
    }

    public void Enable(string name, Guid? parentId)
    {
        _flagManagementService.Enable(name, parentId);
    }

    public void Disable(Guid id)
    {
        _flagManagementService.Disable(id);
    }

    public void Disable(string name, Guid? parentId)
    {
        _flagManagementService.Disable(name, parentId);
    }

    public bool IsOn(string key)
    {
        return _flagService.IsOn(key);
    }

    public bool IsOn(Guid id)
    {
        return _flagService.IsOn(id);
    }
}
