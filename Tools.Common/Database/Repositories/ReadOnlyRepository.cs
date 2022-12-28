using GoSolve.Tools.Api.Database.Repositories.Interfaces;
using GoSolve.Tools.Common.Database.Models;
using GoSolve.Tools.Common.Database.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GoSolve.Tools.Common.Database.Repositories;

/// <summary>
/// Abstract class including the read-only functionality of a database repository.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
/// <typeparam name="TId">Type of the id of the entity.</typeparam>
/// <typeparam name="TDbContext">Type of the DbContext that is used for this entity.</typeparam>
public abstract class ReadOnlyRepository<TEntity, TId, TDbContext> : IReadOnlyRepository<TEntity, TId>
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
    public ReadOnlyRepository(TDbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Gets an entity by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetById(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Finds all entities which match a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().Where(predicate).ToListAsync();
    }
}
