using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;

namespace UltimateFlags.Abstraction.Services;

public interface IFlagService
{
    #region sync

    /// <summary>
    ///     Creates a FLAG.
    /// </summary>
    /// <param name="contract">Request contract of FLAG to create</param>
    /// <returns>Response contract of created FLAG</returns>
    /// <exception cref="FlagDuplicateFound">
    ///     FlagDuplicateFound will be thrown when the FLAG with the same KEY exists.
    /// </exception>
    public FlagResponse Create(FlagCreationRequest contract);

    /// <summary>
    ///     Retrieves a FLAG by the ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Response contract if found / null if not found</returns>
    public FlagResponse? Get(Guid id);

    /// <summary>
    ///     Retrieves a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Response contract</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse GetRequired(Guid id);

    /// <summary>
    ///     Retrieves a FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <returns>Response contract if found / null if not found</returns>
    public FlagResponse? Get(string key);

    /// <summary>
    ///     Retrieves a FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <returns>Response contract</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse GetRequired(string key);

    /// <summary>
    ///     Retrieves a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>Response contract if found / null if not found</returns>
    public FlagResponse? Get(string name, Guid? parentId);

    /// <summary>
    ///     Retrieves a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>Response contract</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse GetRequired(string name, Guid? parentId);

    /// <summary>
    ///     Retrieves a FLAG by NAME and ParentKey.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentKey">ParentKey</param>
    /// <returns>Response contract if found / null if not found</returns>
    public FlagResponse? Get(string name, string? parentKey);

    /// <summary>
    ///     Retrieves a FLAG by NAME and ParentKey.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentKey">ParentKey</param>
    /// <returns>Response contract</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse GetRequired(string name, string? parentKey);

    /// <summary>
    ///     Searches and retrieves FLAGs.
    /// </summary>
    /// <remarks>
    ///     When the search string is passed in,
    ///     The FLAG NAME will be searched with it regardless of the level of the FLAG.
    /// </remarks>
    /// <param name="searchString">Search string</param>
    /// <returns>Response contracts</returns>
    public IEnumerable<FlagResponse> List(string? searchString);

    /// <summary>
    ///     Searches and retrieves FLAGs.
    /// </summary>
    /// <remarks>
    ///     When the search string is passed in,
    ///     The FLAG NAME will be searched.
    ///     When the ParentId is passed in,
    ///     the search will be only for the specified FLAG and children recursively.
    /// </remarks>
    /// <param name="searchString">Search string</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>Response contracts</returns>
    public IEnumerable<FlagResponse> List(string? searchString, Guid? parentId);

    /// <summary>
    ///     Searches and retrieves FLAGs.
    /// </summary>
    /// <remarks>
    ///     When the search string is passed in,
    ///     The FLAG NAME will be searched.
    ///     When the ParentKey is passed in,
    ///     the search will be only for the specified FLAG and children recursively.
    /// </remarks>
    /// <param name="searchString">Search string</param>
    /// <param name="parentKey">ParentKey</param>
    /// <returns>Response contracts</returns>
    public IEnumerable<FlagResponse> List(string? searchString, string? parentKey);

    /// <summary>
    ///     Updates a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="contract">Request contract of FLAG to update</param>
    /// <returns>Response contract of updated FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse Update(Guid id, FlagUpdateRequest contract);

    /// <summary>
    ///     Updates a FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <param name="contract">Request contract of FLAG to update</param>
    /// <returns>Response contract of updated FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse Update(string key, FlagUpdateRequest contract);

    /// <summary>
    ///     Deletes a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Response contract of deleted FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse Delete(Guid id);

    /// <summary>
    ///     Deletes a FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <returns>Response contract of deleted FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public FlagResponse Delete(string key);

    /// <summary>
    ///     Enables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Enable(Guid id);

    /// <summary>
    ///     Enables FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Enable(string key);

    /// <summary>
    ///     Enables a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Enable(string name, Guid? parentId);

    /// <summary>
    ///     Disables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Disable(Guid id);

    /// <summary>
    ///     Disables a FLAG by KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Disable(string key);

    /// <summary>
    ///     Disables FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public void Disable(string name, Guid? parentId);

    /// <summary>
    ///     Checks whether the specified FLAG is ON or OFF independent of its parent.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>TRUE if the specified FLAG is ON / FALSE otherwise. Note that the parent FLAG's status doesn't count.</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public bool IsOn(Guid id);

    /// <summary>
    ///     Checks whether the specified FLAG is ON or OFF taking the hierarchy into account.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <returns>TRUE if the specified FLAG is ON taking the hierarchy into account / FALSE otherwise</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public bool IsOn(string key);

    /// <summary>
    ///     Checks whether the specified FLAG is ON or OFF taking the hierarchy into account.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>TRUE if the specified FLAG is ON taking the hierarchy into account / FALSE otherwise</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID doesn't exist.
    /// </exception>
    public bool IsOn(string name, Guid? parentId);

    #endregion sync

    #region async

    public Task<FlagResponse> CreateAsync(FlagCreationRequest contract, CancellationToken cancellationToken = default);

    public Task<FlagResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<FlagResponse> GetRequiredAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<FlagResponse?> GetAsync(string key, CancellationToken cancellationToken = default);

    public Task<FlagResponse> GetRequiredAsync(string key, CancellationToken cancellationToken = default);

    public Task<FlagResponse?> GetAsync(string name, Guid? parentId, CancellationToken cancellationToken = default);

    public Task<FlagResponse> GetRequiredAsync(string name, Guid? parentId, CancellationToken cancellationToken = default);

    public Task<FlagResponse?> GetAsync(string name, string? parentKey, CancellationToken cancellationToken = default);

    public Task<FlagResponse> GetRequiredAsync(string name, string? parentKey, CancellationToken cancellationToken = default);

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, CancellationToken cancellationToken = default);

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, Guid? parentId, CancellationToken cancellationToken = default);

    public Task<IEnumerable<FlagResponse>> ListAsync(string? searchString, string? parentKey, CancellationToken cancellationToken = default);

    public Task<FlagResponse> UpdateAsync(Guid id, FlagUpdateRequest contract, CancellationToken cancellationToken = default);

    public Task<FlagResponse> UpdateAsync(string key, FlagUpdateRequest contract, CancellationToken cancellationToken = default);

    public Task<FlagResponse> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<FlagResponse> DeleteAsync(string key, CancellationToken cancellationToken = default);

    public Task EnableAsync(Guid id, CancellationToken cancellationToken = default);

    public Task EnableAsync(string key, CancellationToken cancellationToken = default);

    public Task EnableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default);

    public Task DisableAsync(Guid id, CancellationToken cancellationToken = default);

    public Task DisableAsync(string key, CancellationToken cancellationToken = default);

    public Task DisableAsync(string name, Guid? parentId, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(string key, CancellationToken cancellationToken = default);

    public Task<bool> IsOnAsync(string name, Guid? parentId, CancellationToken cancellationToken = default);

    #endregion async
}
