using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UltimateFlags.Abstraction.Config;
using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Entities;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimateFlags.Abstraction.Storages;
using UltimateFlags.Helpers;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Managers;

internal class FlagManager : IFlagManager
{
    private readonly ILogger<FlagManager> _logger;

    private readonly IFlagStorage _flagStorage;

    private readonly UltimateFlagConfiguration _ultimateFlagConfiguration;

    public FlagManager(
        ILogger<FlagManager> logger,
        IFlagStorage flagStorage,
        IOptions<UltimateFlagConfiguration> options)
    {
        _logger = logger;
        _flagStorage = flagStorage;
        _ultimateFlagConfiguration = options.Value;
    }

    #region sync

    public Flag Create(string name, Guid? parentId, bool isOn)
    {
        DateTime utcNow = DateTime.UtcNow;

        Flag entity =
            new()
            {
                Name = name,
                ParentId = parentId,
                IsOn = isOn,
                CreatedAt = utcNow,
                UpdatedAt = utcNow,
                DeletedAt = null,
            };

        return _flagStorage.Create(entity);
    }

    public Flag? Get(Guid id)
    {
        return  _flagStorage.Get(id);
    }

    public Flag? Get(string key)
    {
        throw new NotImplementedException();
    }

    public Flag? Get(string name, Guid? parentId)
    {
        return _flagStorage.Get(name, parentId);
    }

    public IPagedList<Flag> List(
        string? searchString,
        bool? isOn,
        int? pageNumber,
        int? pageSize)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        bool? isOn,
        int? pageNumber,
        int? pageSize)
    {
        throw new NotImplementedException();
    }

    public IPagedList<Flag> List(
        string? searchString,
        string? parentKey,
        bool? isOn,
        int? pageNumber,
        int? pageSize)
    {
        throw new NotImplementedException();
    }

    public Flag Update(Guid id, FlagUpdateRequest contract)
    {
        Flag entity =
            _flagStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Update)}(id, contract)",
            };

        return _flagStorage.Update(entity.UpdateFrom(contract));
    }

    public Flag Update(string key, FlagUpdateRequest contract)
    {
        throw new NotImplementedException();
    }

    public Flag Delete(Guid id)
    {
        return _flagStorage.Delete(id);
    }

    public Flag Delete(string key)
    {
        throw new NotImplementedException();
    }

    public void Enable(Guid id)
    {
        Flag entity =
            _flagStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Enable)}(id)",
            };

        if (!entity.IsOn)
        {
            _flagStorage.Update(entity.Enabled());
        }
    }

    public void Enable(string key)
    {
        throw new NotImplementedException();
    }

    public void Enable(string name, Guid? parentId)
    {
        Flag entity =
            _flagStorage.Get(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Enable)}(name, parentId)",
            };

        if (!entity.IsOn)
        {
            _flagStorage.Update(entity.Enabled());
        }
    }

    public void Disable(Guid id)
    {
        Flag entity =
            _flagStorage.Get(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Disable)}(id)",
            };

        if (entity.IsOn)
        {
            _flagStorage.Update(entity.Disabled());
        }
    }

    public void Disable(string key)
    {
        throw new NotImplementedException();
    }

    public void Disable(string name, Guid? parentId)
    {
        Flag entity =
            _flagStorage.Get(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(Disable)}(name, parentId)",
            };

        if (entity.IsOn)
        {
            _flagStorage.Update(entity.Disabled());
        }
    }

    public bool IsOn(Guid id)
    {
        Flag entity =
            _flagStorage.Read(id)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(id)",
            };

        return entity.IsOn;
    }

    public bool IsOn(string key)
    {
        throw new NotImplementedException();
    }

    public bool IsOn(string name, Guid? parentId)
    {
        Flag entity =
            _flagStorage.Read(name, parentId)
            ?? throw new FlagNotFound
            {
                Area = $"{nameof(FlagManager)}.{nameof(IsOn)}(name, parentId)",
            };

        return entity.IsOn;
    }

    public int SaveChanges()
    {
        return _flagStorage.SaveChanges();
    }

    #endregion sync

    #region async

    public Task<Flag> CreateAsync(
        string name,
        Guid? parentId,
        bool isOn,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag?> GetAsync(
        string name,
        Guid? parentId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> DeleteAsync(
        Guid id,
        CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<Flag> DeleteAsync(
        string key,
        CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _flagStorage.SaveChangesAsync(cancellationToken);
    }

    #endregion async
}
