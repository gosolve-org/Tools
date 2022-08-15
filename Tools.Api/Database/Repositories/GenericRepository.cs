using GoSolve.Tools.Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoSolve.Tools.Api.Database.Repositories;

/// <summary>
/// Abstract class including the default functionality of a database repository.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
/// <typeparam name="TId">Type of the id of the entity.</typeparam>
/// <typeparam name="TDbContext">Type of the DbContext that is used for this entity.</typeparam>
public abstract class GenericRepository<TEntity, TId, TDbContext> : IGenericRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TDbContext : BaseDbContext<TDbContext>
{
    /// <summary>
    /// The database context.
    /// </summary>
    protected TDbContext Context { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context"></param>
    public GenericRepository(TDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Gets an entity by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity> GetById(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Finds all entities which match a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Begins tracking the given entity, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the entity to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entity"></param>
    public void Add(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Begins tracking the given entities, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the entity to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entities"></param>
    public void AddRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    /// <summary>
    /// Begins tracking the deletion of the given entity, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the deletion of this entity to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    /// <summary>
    /// Begins tracking the deletion of the given entities, and any other reachable entities that are not already being tracked.
    /// NOTE: This does not save the deletion of these entities to the database, a <see cref="IUnitOfWork" /> still needs to be completed.
    /// </summary>
    /// <param name="entities"></param>
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
}
