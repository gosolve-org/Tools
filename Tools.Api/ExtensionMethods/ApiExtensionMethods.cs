using GoSolve.Tools.Api.ProblemDetails;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Api.ExtensionMethods;

/// <summary>
/// Api extension methods.
/// </summary>
public static class ApiExtensionMethods
{
    /// <summary>
    /// Adds default api tools and configurations.
    /// </summary>
    /// <param name="services"></param>
    public static void AddApiTools(this IServiceCollection services)
    {
        services.AddProblemDetails(problemDetailsOptions =>
            ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(new ProblemDetailsOptionsProxy(problemDetailsOptions)));
    }

    /// <summary>
    /// Registers default api tools and configurations.
    /// </summary>
    /// <param name="app"></param>
    public static void UseApiTools(this IApplicationBuilder app)
    {
        app.UseProblemDetails();
    }
}
