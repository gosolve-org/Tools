using GoSolve.Tools.Api.Json;
using GoSolve.Tools.Api.ProblemDetails;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        services.AddSwaggerGen();
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddApiVersioning(options =>
        {
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
        });
        services.AddProblemDetails(problemDetailsOptions =>
            ProblemDetailsConfigurationFactory
                .CreateDefaultProblemDetailsOptionsConfiguration(new ProblemDetailsOptionsProxy(problemDetailsOptions)));
        services
            .AddControllers()
            .AddCustomJsonOptions();
    }

    /// <summary>
    /// Registers default api tools and configurations.
    /// </summary>
    /// <param name="app"></param>
    public static void UseApiTools(this WebApplication app)
    {
        var configuration = app.Configuration;

        var pathBase = configuration.GetValue<string>("PathBase");
        if (!string.IsNullOrWhiteSpace(pathBase))
        {
            app.UsePathBase(pathBase);
        }

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
