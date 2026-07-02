using Microsoft.AspNetCore.Mvc;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Services;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Example.EF.Host.Controllers;

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
    [EndpointName("CreateFlag")]
    public FlagResponse Create([FromBody] FlagCreationRequest contract)
    {
        return _flagService.Create(contract);
    }

    [HttpGet]
    [Route("{id}")]
    [EndpointName("GetFlag")]
    public FlagResponse Get([FromRoute] Guid id)
    {
        return _flagService.GetRequired(id);
    }

    [HttpGet]
    [Route("")]
    [EndpointName("ListFlags")]
    public IPagedList<FlagResponse> List(
        [FromQuery] string? search = null,
        [FromQuery] bool? isOn = null,
        [FromQuery] int? pageNumber = null,
        [FromQuery] int? pageSize = null)
    {
        return
            _flagService
                .List(
                    search,
                    isOn,
                    pageNumber,
                    pageSize);
    }

    [HttpPut]
    [Route("{id}")]
    [EndpointName("UpdateFlag")]
    public FlagResponse Update(
        [FromRoute] Guid id,
        [FromBody] FlagUpdateRequest contract)
    {
        return _flagService.Update(id, contract);
    }

    [HttpDelete]
    [Route("{id}")]
    [EndpointName("DeleteFlag")]
    public int Delete([FromRoute] Guid id)
    {
        return _flagService.Delete(id);
    }
}
