using GoSolve.Tools.Common.Database.Models;

namespace GoSolve.Tools.Common.Database.Seeding.Interfaces;

/// <summary>
/// Interface defining a data seeder.
/// </summary>
public interface IDataSeeder
{
    /// <summary>
    /// Builds the data to be seeded.
    /// </summary>
    /// <returns></returns>
    IEnumerable<TimestampedEntity> BuildData();
}
