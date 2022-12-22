using GoSolve.Tools.Common.Database.Models;
using GoSolve.Tools.Common.Database.Models.Interfaces;
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
    public static void AddDatabaseTools<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : BaseDbContext<TDbContext>
    {
        services.AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });

        services.AddTransient<IUnitOfWork, UnitOfWork<TDbContext>>();
    }
}
