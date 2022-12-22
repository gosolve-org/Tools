using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Common.ExtensionMethods;

public static class ExtensionMethods
{
    public static IServiceCollection AddCommonTools(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(lb => lb.SetupSerilog(configuration));

        return services;
    }
}
