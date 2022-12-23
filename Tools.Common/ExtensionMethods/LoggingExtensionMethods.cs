using GoSolve.Tools.Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GoSolve.Tools.Common.ExtensionMethods;

/// <summary>
/// Extension methods for logging functionality.
/// </summary>
public static class LoggingExtensionMethods
{
    /// <summary>
    /// Sets up default Serilog logging.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static ILoggingBuilder SetupSerilog(this ILoggingBuilder builder, IConfiguration configuration)
    {
        Log.Logger = SerilogFactory.CreateLogger(configuration);
        return builder.AddSerilog(Log.Logger, dispose: true);
    }
}
