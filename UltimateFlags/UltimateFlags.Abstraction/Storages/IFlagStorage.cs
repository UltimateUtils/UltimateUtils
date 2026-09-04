using UltimateFlags.Abstraction.Entities;
using UltimatePagination.Abstraction;

namespace UltimateFlags.Abstraction.Storages;

public interface IFlagStorage
{
    /// <summary>
    ///     Creates a FLAG.
    /// </summary>
    /// <param name="flag">An entity of the FLAG</param>
    /// <returns>An entity of the created FLAG</returns>
    public Flag Create(Flag flag);

    /// <summary>
    ///     Retrieves a readonly FLAG with NO TRACKING by the ID.
    /// </summary>
    /// <remarks>
    ///     This method has NO TRACKING when used with EF context.
    ///     With non-EF context, this method is the same as Get().
    /// </remarks>
    /// <param name="id">ID</param>
    /// <returns>FLAG entity if found / null if not found</returns>
    public Flag? Read(Guid id);

    /// <summary>
    ///     Retrieves a readonly FLAG with NO TRACKING by the NAME and the ParentId.
    /// </summary>
    /// <remarks>
    ///     This method has No TRACKING when used with EF context.
    ///     With non-EF context, this method is the same as Get().
    /// </remarks>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>FLAG entity if found / null if not found</returns>
    public Flag? Read(string name, Guid? parentId);

    /// <summary>
    ///     Retrieves a FLAG with TRACKING by the ID.
    /// </summary>
    /// <remarks>
    ///     This method has TRACKING when used with EF context.
    ///     With non-EF context, this method is the same as Read().
    /// </remarks>
    /// <param name="id">ID</param>
    /// <returns>FLAG entity if found / null if not found</returns>
    public Flag? Get(Guid id);

    /// <summary>
    ///     Retrieves a FLAG with TRACKING by the NAME and the ParentId.
    /// </summary>
    /// <remarks>
    ///     This method has TRACKING when used with EF context.
    ///     With non-EF context, this method is the same as Read().
    /// </remarks>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>FLAG entity if found / null if not found</returns>
    public Flag? Get(string name, Guid? parentId);

    /// <summary>
    ///     Searches and retrieves FLAGs.
    /// </summary>
    /// <remarks>
    ///     When the search string is passed in,
    ///     The FLAG NAME will be searched.
    ///     When the ParentId is passed in,
    ///     the search will be only for the specified FLAG and children recursively.
    /// </remarks>
    /// <param name="searchString">Search String</param>
    /// <param name="parentId">ParentId</param>
    /// <param name="isOn">Is ON by key</param>
    /// <param name="pageNumber">Page Number</param>
    /// <param name="pageSize">Page Size</param>
    /// <returns>Entities of the FLAGs with the specified   string in their NAMEs under the specified FLAG by ID</returns>
    public IPagedList<Flag> List(
        string? searchString,
        Guid? parentId,
        bool? isOn,
        int pageNumber,
        int pageSize);

    /// <summary>
    ///     Updates a FLAG.
    /// </summary>
    /// <param name="flag">An entity of the FLAG</param>
    /// <returns>An entity of the updated FLAG</returns>
    public Flag Update(Flag flag);

    /// <summary>
    ///     Soft-deletes a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>An entity of the deleted FLAG</returns>
    public Flag Delete(Flag id);

    /// <summary>
    ///     Purges/Hard-deletes a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Number of purged/hard-deleted flags. 1 if successful. 0 Otherwise.</returns>
    public int Purge(Guid id);

    /// <summary>
    ///     Purges/Hard-deletes FLAGs.
    /// </summary>
    /// <param name="fromInclusive">Deleted after (inclusive) the specified time. No limit if NULL.</param>
    /// <param name="toInclusive">Deleted before (inclusive) the specified time. No limit if NULL.</param>
    /// <returns>Number of purged/hard-deleted flags. 1 if successful. 0 Otherwise.</returns>
    public int Purge(DateTime? fromInclusive, DateTime? toInclusive);

    /// <summary>
    ///     Enables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Number of enabled flags. 1 if successful. 0 Otherwise.</returns>
    public int Enable(Guid id);

    /// <summary>
    ///     Enables a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>Number of enabled flags. 1 if successful. 0 Otherwise.</returns>
    public int Enable(string name, Guid? parentId);

    /// <summary>
    ///     Disables a FLAG by ID.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>Number of disabled flags. 1 if successful. 0 Otherwise.</returns>
    public int Disable(Guid id);

    /// <summary>
    ///     Disables a FLAG by NAME and ParentId.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>Number of disabled flags. 1 if successful. 0 Otherwise.</returns>
    public int Disable(string name, Guid? parentId);

    /// <summary>
    ///     Checks whether the FLAG is ON or OFF independent of its parent.
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>TRUE if the FLAG is ON / FALSE otherwise. Note that the result doesn't depend on the parent FLAG.</returns>
    public bool IsOn(Guid id);

    /// <summary>
    ///     Checks whether the FLAG is ON or OFF taking the hierarchy into account.
    /// </summary>
    /// <param name="name">NAME</param>
    /// <param name="parentId">ParentId</param>
    /// <returns>TRUE if the FLAG is ON taking the hierarchy into account / FALSE otherwise</returns>
    public bool IsOn(string name, Guid? parentId);

    /// <summary>
    ///     Saves changes to the database.
    /// </summary>
    /// <remarks>
    ///     If you implement for non-EF context, follow either approaches below:
    ///     1. Do nothing with this method
    ///         - Keep this implementation empty (not throwing NotImplementedException)
    ///         - Save changes to the DB immediately when you make changes in Create(), Update(), Delete(), Enable() and Disable().
    ///
    ///     2. You can make the implementation just like the DbContext.SaveChanges().
    ///         - Save DB changes with this method call.
    ///         - FlagService's Create, Update, Delete, Enable and Disable methods will call SaveChanges() at the end of their implementations.
    /// </remarks>
    /// <returns>The number of state entries written to the database</returns>
    public int SaveChanges();
}
