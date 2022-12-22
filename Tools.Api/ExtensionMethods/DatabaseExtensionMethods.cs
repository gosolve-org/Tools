using GoSolve.Tools.Common.Database.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    public static void MigrateDatabase<TDbContext>(this WebApplication app)
        where TDbContext : BaseDbContext<TDbContext>
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<TDbContext>();
            db.Database.Migrate();
        }
    }
}
