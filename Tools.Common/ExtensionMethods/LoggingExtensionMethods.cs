using GoSolve.Tools.Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GoSolve.Tools.Common.ExtensionMethods;

public static class LoggingExtensionMethods
{
    public static ILoggingBuilder SetupSerilog(this ILoggingBuilder builder, IConfiguration configuration)
    {
        Log.Logger = SerilogFactory.CreateLogger(configuration);
        return builder.AddSerilog(Log.Logger, dispose: true);
    }
}
