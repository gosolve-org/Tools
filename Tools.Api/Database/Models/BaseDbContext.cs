using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GoSolve.Tools.Api.Database.Models;

/// <summary>
/// The base DbContext class which injects functionality during saving, such as adding Timestamps.
/// </summary>
/// <typeparam name="T">The type of the class that is inheriting from this BaseDbContext class.</typeparam>
public abstract class BaseDbContext<T> : DbContext
    where T : BaseDbContext<T>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    public BaseDbContext(DbContextOptions<T> options)
        : base(options)
    {
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is TimestampedEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((TimestampedEntity)entity.Entity).CreatedAt = now;
            }

            ((TimestampedEntity)entity.Entity).UpdatedAt = now;
        }
    }
}
