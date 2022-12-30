using GoSolve.Tools.Common.Database.Seeding.Interfaces;

namespace GoSolve.Tools.Common.Database.Seeding;

/// <summary>
/// Used to build a collection of seeders.
/// </summary>
/// <typeparam name="TSeeder">The generic type of seeders to add in this builder.</typeparam>
public class DataSeederBuilder<TSeeder>
    where TSeeder : IDataSeeder
{
    private readonly List<TSeeder> _seeders;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DataSeederBuilder()
    {
        _seeders = new List<TSeeder>();
    }

    /// <summary>
    /// Adds a seeder to the builder.
    /// </summary>
    /// <typeparam name="TSeederImplemenation">The type of your seeder to add.</typeparam>
    /// <returns></returns>
    public DataSeederBuilder<TSeeder> AddSeeder<TSeederImplemenation>()
        where TSeederImplemenation : TSeeder, new()
    {
        _seeders.Add(new TSeederImplemenation());

        return this;
    }

    /// <summary>
    /// Builds the list of seeders.
    /// </summary>
    /// <returns></returns>
    public List<TSeeder> Build()
    {
        return _seeders;
    }
}
