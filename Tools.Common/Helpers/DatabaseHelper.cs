using GoSolve.Tools.Common.Database.Models;

namespace GoSolve.Tools.Common.Helpers;

/// <summary>
/// Helper class for databases.
/// </summary>
public static class DatabaseHelper
{
    /// <summary>
    /// Adds missing auto-generated timestamps to entities.
    /// Note: Our tooling automatically generates these properties. This should only be used in a non-standard way of adding/updating entities.
    /// </summary>
    /// <param name="entities"></param>
    public static void AddMissingTimestampedProperties(IEnumerable<TimestampedEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreatedAt == default)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            if (entity.UpdatedAt == default)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
