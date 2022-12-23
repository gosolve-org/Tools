using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Common.ExtensionMethods;

/// <summary>
/// Common goSolve pipeline extension methods.
/// </summary>
public static class PipelineExtensionMethods
{
    /// <summary>
    /// Add the common goSolve tools.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddCommonTools(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(lb => lb.SetupSerilog(configuration));

        return services;
    }
}
