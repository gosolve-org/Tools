using GoSolve.Tools.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Database.ExtensionMethods;

/// <summary>
/// Database extension methods.
/// </summary>
public static class DatabaseExtensionMethods
{
    /// <summary>
    /// Adds default database tools and configurations.
    /// </summary>
    /// <param name="services"></param>
    public static void AddDatabaseTools<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : BaseDbContext<TDbContext>
    {
        services.AddEntityFrameworkNpgsql().AddDbContext<TDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("dbConnection"));
        });
    }

    /// <summary>
    /// Registers default api tools and configurations.
    /// </summary>
    /// <param name="app"></param>
    public static void UseApiTools(this WebApplication app)
    {
        app.UseRouting();
        app.UseProblemDetails();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        // app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
    }
}
