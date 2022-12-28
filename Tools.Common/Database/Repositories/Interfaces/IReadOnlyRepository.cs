using System.Linq.Expressions;
using GoSolve.Tools.Common.Database.Models;
using GoSolve.Tools.Common.Database.Models.Interfaces;

namespace GoSolve.Tools.Api.Database.Repositories.Interfaces;

/// <summary>
/// Interface for the read-only functionalities of a database repository.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
/// <typeparam name="TId">Type of the id of the entity.</typeparam>
public interface IReadOnlyRepository<TEntity, TId>
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
}
