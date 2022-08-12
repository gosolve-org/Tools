﻿using GoSolve.Tools.Api.Database.Models;
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
    /// Adds default database tools.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to register.</typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddDatabaseTools<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : BaseDbContext<TDbContext>
    {
        services.AddEntityFrameworkNpgsql().AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });
    }

    /// <summary>
    /// Registers default database tools and migrates the database.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="app"></param>
    public static void UseDatabaseTools<TDbContext>(this WebApplication app)
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