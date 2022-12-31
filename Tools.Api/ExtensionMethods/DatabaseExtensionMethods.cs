using GoSolve.Tools.Common.Database.Models;
using GoSolve.Tools.Common.Database.Seeding;
using GoSolve.Tools.Common.Database.Seeding.Interfaces;
using GoSolve.Tools.Common.Globals;
using GoSolve.Tools.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GoSolve.Tools.Api.ExtensionMethods;

/// <summary>
/// Extension methods for databases.
/// </summary>
public static class DatabaseExtensionMethods
{
    /// <summary>
    /// Registers default database tools and migrates the database.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication MigrateDatabase<TDbContext>(this WebApplication app)
        where TDbContext : BaseDbContext<TDbContext>
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TDbContext>();
            StartupState.DatabaseCreatedOnStartup = !context.Database.CanConnect();
            context.Database.Migrate();
            StartupState.DatabaseMigratedOnStartup = true;
        }

        return app;
    }

    /// <summary>
    /// Seeds test data provided by seeders configured through the builder.
    /// Note: This should only be used in a development environment.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="app"></param>
    /// <param name="configureBuilder"></param>
    /// <returns></returns>
    public static WebApplication SeedTestData<TDbContext>(this WebApplication app, Action<DataSeederBuilder<ITestDataSeeder>> configureBuilder)
        where TDbContext : BaseDbContext<TDbContext>
    {
        if (!StartupState.DatabaseMigratedOnStartup)
        {
            throw new Exception($"{nameof(MigrateDatabase)} needs to be run before seeding test data.");
        }

        if (!EnvironmentHelper.IsDevelopment())
        {
            Log.Logger.Warning($"{nameof(SeedTestData)} was called in a non-development environment. Test data will not be seeded.");
            return app;
        }

        if (!StartupState.DatabaseCreatedOnStartup.HasValue)
        {
            Log.Logger.Warning($"Expected value for {nameof(StartupState.DatabaseCreatedOnStartup)}, but was null instead. Test data will not be seeded.");
            return app;
        }

        if (StartupState.DatabaseCreatedOnStartup == false)
        {
            Log.Logger.Information("Database already exists. Test data will not be seeded.");
            return app;
        }

        var builder = new DataSeederBuilder<ITestDataSeeder>();
        configureBuilder(builder);
        var seeders = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TDbContext>();
            Log.Logger.Information("Running test data seeder for new database creation.");
            foreach (var seeder in seeders)
            {
                Log.Logger.Information($"Seeding test data with ITestDataSeeder instance: {seeder.GetType().Name}.");

                var data = seeder.BuildData();

                context.AddRange(data);
                context.SaveChanges();
            }
        }

        return app;
    }
}
