using UltimateFlags.Abstraction.Contracts;

namespace UltimateFlags.Abstraction.Services;

public interface IFlagService
{
    #region sync

    public FlagResponse Create(FlagCreationRequest contract);

    public FlagResponse Read(string key);

    public IEnumerable<FlagResponse> List();

    public FlagResponse Update(string key, FlagUpdateRequest contract);

    public FlagResponse Delete(string key);

    public void Enable(string key);

    public bool IsOn(string key);

    #endregion sync

    #region async

    public Task<FlagResponse> CreateAsync(FlagCreationRequest flag, CancellationToken cancellationToken = default);

    public Task<FlagResponse> ReadAsync(string key, CancellationToken cancellationToken = default);

    public Task<IEnumerable<FlagResponse>> ListAsync(CancellationToken cancellationToken = default);

    public Task<FlagResponse> UpdateAsync(string key, FlagUpdateRequest contract, CancellationToken cancellationToken = default);

    public Task<FlagResponse> DeleteAsync(string key, CancellationToken cancellationToken = default);

    public Task EnableAsync(string key, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default);

    #endregion async
}
