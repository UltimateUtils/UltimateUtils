using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Storages;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Storages;

internal class FlagStorage : IFlagStorage
{
    private readonly ILogger<FlagStorage> _logger;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagStorage(
        ILogger<FlagStorage> logger,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _ultimateFlagConfiguration = options.Value;
    }

    public Flag Create(Flag flag)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(Guid id)
    {
        throw new NotImplementedException();
    }

    public Flag? Read(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        bool? isOn,
        int pageNumber,
        int pageSize)
    {
        throw new NotImplementedException();
    }

    public Flag Update(Flag flag)
    {
        throw new NotImplementedException();
    }

    public int Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public int Purge(Guid id)
    {
        throw new NotImplementedException();
    }

    public int Purge(DateTime? fromInclusive, DateTime? toInclusive)
    {
        throw new NotImplementedException();
    }

    public int Enable(Guid id)
    {
        throw new NotImplementedException();
    }

    public int Enable(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public int Disable(Guid id)
    {
        throw new NotImplementedException();
    }

    public int Disable(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }
}
