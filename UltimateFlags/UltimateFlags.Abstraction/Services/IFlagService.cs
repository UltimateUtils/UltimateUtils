using UltimateFlags.Abstraction.Contracts;
using UltimateFlags.Abstraction.Exceptions.ClientFaults;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Abstraction.Services;

public interface IFlagService
{
    /// <summary>
    ///     Creates a FLAG.
    /// </summary>
    /// <param name="contract">Request contract of FLAG to create by Name and ParentId</param>
    /// <returns>Response contract of created FLAG</returns>
    /// <exception cref="FlagDuplicateFound">
    ///     FlagDuplicateFound will be thrown when the FLAG with the same KEY exists.
    /// </exception>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG specified by the ParentId does not exist.
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
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public FlagResponse GetRequired(Guid id);

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
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public FlagResponse GetRequired(string name, Guid? parentId);

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
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public FlagResponse GetRequired(string key);

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
    /// <param name="isOn">Is ON by key</param>
    /// <param name="pageNumber">Page Number</param>
    /// <param name="pageSize">Page Size</param>
    /// <returns>Response contracts</returns>
    public IPagedList<FlagResponse> List(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null);

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
    /// <param name="isOn">Is ON by key</param>
    /// <param name="pageNumber">Page Number</param>
    /// <param name="pageSize">Page Size</param>
    /// <returns>Response contracts</returns>
    public IPagedList<FlagResponse> Search(
        string? searchString = null,
        bool? isOn = null,
        int? pageNumber = null,
        int? pageSize = null);

    /// <summary>
    ///     Updates a FLAG by ID. Only name may be updated.
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="contract">Request contract of FLAG to update</param>
    /// <returns>Response contract of updated FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public FlagResponse Update(Guid id, FlagUpdateRequest contract);

    /// <summary>
    ///     Soft-Deletes a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Response contract of deleted FLAG</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public FlagResponse Delete(Guid id);

    /// <summary>
    ///     Purge/Hard-Deletes a FLAG
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Number of purged/hard-deleted flags. 1 if successful. 0 Otherwise.</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    /// <exception cref="FlagNotDeleted">
    ///     FlagNotDeleted will be thrown when the FLAG is not soft-deleted.
    /// </exception>
    public int Purge(Guid id);

    /// <summary>
    ///     Purge/Hard-Deletes FLAGs
    /// </summary>
    /// <param name="fromInclusive">Deleted after (inclusive) the specified time. No limit if NULL.</param>
    /// <param name="toInclusive">Deleted before (inclusive) the specified time. No limit if NULL.</param>
    /// <returns>Number of purged/hard-deleted flags.</returns>
    public int Purge(DateTime? fromInclusive = null, DateTime? toInclusive = null);

    /// <summary>
    ///     Enables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public void Enable(Guid id);

    /// <summary>
    ///     Enables a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public void Enable(string name, Guid? parentId);

    /// <summary>
    ///     Disables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public void Disable(Guid id);

    /// <summary>
    ///     Disables a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public void Disable(string name, Guid? parentId);

    /// <summary>
    ///     Checks whether the specified KEY is ON or OFF by checking the hierarchy of the flags represented by the KEY.
    /// </summary>
    /// <param name="key">KEY</param>
    /// <returns>TRUE if the specified chain of the FLAGs are all ON / FALSE otherwise</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public bool IsOn(string key);

    /// <summary>
    ///     Checks whether the specified FLAG is ON or OFF.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>TRUE if the specified FLAG is ON / FALSE otherwise</returns>
    /// <exception cref="FlagNotFound">
    ///     FlagNotFound will be thrown when the FLAG with the ID does not exist.
    /// </exception>
    public bool IsOn(Guid id);
}
