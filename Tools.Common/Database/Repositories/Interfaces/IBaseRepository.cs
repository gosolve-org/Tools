﻿using Microsoft.EntityFrameworkCore;

namespace GoSolve.Tools.Common.Database.Repositories.Interfaces;

/// <summary>
/// The base repository interface that should be used by all repository interfaces.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    /// Prepares the default entity source that will be used in the default read operations.
    /// Note: This will not have effect on overridden methods.
    /// </summary>
    /// <typeparam name="TQueryable">Queryable type to prepare.</typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TQueryable PrepareDefaultReadSource<TQueryable>(TQueryable source)
        where TQueryable : IQueryable<TEntity>;
}
