namespace Expensier.Domain.Services
{
    /// <summary>
    /// Represents a generic service interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IDataService<T>
    {
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        Task<T> Create( T entity );

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>The updated entity.</returns>
        Task<T> Update( Guid id, T entity );

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>True if the entity was successfully deleted; otherwise, false.</returns>
        Task<bool> Delete( Guid id );

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The entity corresponding to the provided identifier.</returns>
        Task<T> GetByID( Guid id );

        /// <summary>
        /// Retrieves all entities of the specified type.
        /// </summary>
        /// <returns>A collection containing all entities of the specified type.</returns>
        Task<IEnumerable<T>> GetAll();
    }
}
