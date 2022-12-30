using GoSolve.Tools.Common.Database.Models;
using GoSolve.Tools.Common.Database.Models.Interfaces;
using GoSolve.Tools.Common.Database.Seeding;
using GoSolve.Tools.Common.Database.Seeding.Interfaces;
using GoSolve.Tools.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Common.ExtensionMethods;

/// <summary>
/// Extension methods for databases.
/// </summary>
public static class DatabaseExtensionMethods
{
    /// <summary>
    /// Adds default database tools.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to register.</typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseTools<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : BaseDbContext<TDbContext>
    {
        services.AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });

        services.AddTransient<IUnitOfWork, UnitOfWork<TDbContext>>();

        return services;
    }

    /// <summary>
    /// Seeds core data of the application to the database.
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="configureBuilder"></param>
    public static void SeedCoreData(this ModelBuilder modelBuilder, Action<DataSeederBuilder<ICoreDataSeeder>> configureBuilder)
    {
        var builder = new DataSeederBuilder<ICoreDataSeeder>();
        configureBuilder(builder);
        var seeders = builder.Build();

        foreach (var seeder in seeders)
        {
            var data = seeder.BuildData();
            DatabaseHelper.AddMissingTimestampedProperties(data);

            if (data.Count() == 0)
            {
                continue;
            }

            modelBuilder.Entity(data.First().GetType()).HasData(data);
        }
    }
}
