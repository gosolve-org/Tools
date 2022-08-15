using System.Linq.Expressions;
using GoSolve.Tools.Api.Database.Models;

namespace GoSolve.Tools.Api.Database.Repositories;

/// <summary>
/// Interface for the default functionality of a database repository.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
/// <typeparam name="TId">Type of the id of the entity.</typeparam>
public interface IGenericRepository<TEntity, TId>
{
    /// <summary>
    /// Gets an entity by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetById(TId id);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAll();

    /// <summary>
    /// Finds all entities which match a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Begins tracking the given entity, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the entity to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entity"></param>
    void Add(TEntity entity);

    /// <summary>
    /// Begins tracking the given entities, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the entities to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entities"></param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Begins tracking the deletion of the given entity, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the deletion of this entity to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entity"></param>
    void Remove(TEntity entity);

    /// <summary>
    /// Begins tracking the deletion of the given entities, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the deletion of these entities to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entities"></param>
    void RemoveRange(IEnumerable<TEntity> entities);
}
