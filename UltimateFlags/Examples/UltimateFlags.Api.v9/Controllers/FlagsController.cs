using Microsoft.AspNetCore.Mvc;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Api.v9.Services.Abstraction;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Api.v9.Controllers;

[ApiController]
[Route("flags")]
public class FlagsController : ControllerBase
{
    private readonly ILogger<FlagsController> _logger;

    private readonly IFlagService _flagService;

    public FlagsController(
        ILogger<FlagsController> logger,
        IFlagService flagService)
    {
        _logger = logger;
        _flagService = flagService;
    }

    [HttpPost]
    [Route("")]
    public FlagResponse Create(FlagCreationRequest contract)
    {
        return _flagService.Create(contract);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public FlagResponse GetById([FromRoute] Guid id)
    {
        return _flagService.GetRequired(id);
    }

    [HttpGet]
    [Route("")]
    public FlagResponse Get([FromQuery] string name, [FromQuery] Guid? parentId)
    {
        return _flagService.GetRequired(name, parentId);
    }

    [HttpGet]
    [Route("{key}")]
    public FlagResponse Get([FromRoute] string key)
    {
        return _flagService.GetRequired(key);
    }

    [HttpGet]
    [Route("list")]
    public IPagedList<FlagResponse> List(
        [FromQuery] string? searchString = null,
        [FromQuery] bool? isOn = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        return
            _flagService
                .List(
                    searchString,
                    isOn,
                    pageNumber,
                    pageSize);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public FlagResponse Update([FromRoute] Guid id, [FromBody] FlagUpdateRequest contract)
    {
        return _flagService.Update(id, contract);
    }

    [HttpPut]
    [Route("execute-update/{id:guid}")]
    public int ExecuteUpdate([FromRoute] Guid id, [FromBody] FlagUpdateRequest contract)
    {
        return _flagService.ExecuteUpdate(id, contract);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public FlagResponse Delete([FromRoute] Guid id, [FromQuery] bool purge = false)
    {
        return
            purge
                ? _flagService.Purge(id)
                : _flagService.Delete(id);
    }

    [HttpDelete]
    [Route("execute-delete/{id:guid}")]
    public int ExecuteDelete([FromRoute] Guid id, [FromQuery] bool purge = false)
    {
        return
            purge
                ? _flagService.ExecutePurge(id)
                : _flagService.ExecuteDelete(id);
    }

    [HttpDelete]
    [Route("execute-purge")]
    public int ExecutePurge([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        return _flagService.ExecutePurge(from, to);
    }

    [HttpPut]
    [Route("{id:guid}/enable")]
    public void EnableById([FromRoute] Guid id)
    {
        _flagService.Enable(id);
    }

    [HttpPut]
    [Route("enable")]
    public void EnableByName([FromQuery] string name, [FromQuery] Guid? parentId)
    {
        _flagService.Enable(name, parentId);
    }

    [HttpPut]
    [Route("{id:guid}/disable")]
    public void DisableById([FromRoute] Guid id)
    {
        _flagService.Disable(id);
    }

    [HttpPut]
    [Route("disable")]
    public void DisableByName([FromQuery] string name, [FromQuery] Guid? parentId)
    {
        _flagService.Disable(name, parentId);
    }

    [HttpGet]
    [Route("{key}/is-on")]
    public bool IsOnByKey([FromRoute] string key)
    {
        return _flagService.IsOn(key);
    }

    [HttpGet]
    [Route("{id:guid}/is-on")]
    public bool IsOnByKey([FromRoute] Guid id)
    {
        return _flagService.IsOn(id);
    }
}
