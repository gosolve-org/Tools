using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace GoSolve.Tools.Common.Logging;

/// <summary>
/// Factory for building Serilog configurations.
/// </summary>
public static class SerilogFactory
{
    /// <summary>
    /// Creates a Serilog logger.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Logger CreateLogger(IConfiguration configuration)
    {
        var loggingMinLevel = GetCurrentLoggingMinimumLevel(configuration);

        // If our minimum log level is Information, then we want the minimum log level for detailed logs to be Warning (to avoid too much & useless logging).
        // Otherwise, we want the log level for detailed logs to be the same as the configured minimum log level (or Information by default).
        var detailedLogsMinLevel = loggingMinLevel.HasValue && loggingMinLevel.Value == LogEventLevel.Information
            ? LogEventLevel.Warning
            : loggingMinLevel ?? LogEventLevel.Information;

        if (!loggingMinLevel.HasValue)
        {
            Log.Logger.Error("Application does not have Serilog.MinimumLevel set, application will continue with .NET Core's default value of Information.");
        }

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerHandler", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authorization.DefaultAuthorizationService", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", detailedLogsMinLevel)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Infrastructure", detailedLogsMinLevel)
            .MinimumLevel.Override("Host", detailedLogsMinLevel)
            .MinimumLevel.Override("Function", detailedLogsMinLevel)
            .MinimumLevel.Override("Azure", detailedLogsMinLevel)
            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error) // Function apps v4 contain a bug logging warnings which should be ignored.
            .Filter.ByExcluding(log =>
            {
                if (log == null)
                {
                    return false;
                }

                var source = GetLogPropertyValue<string>(log, "SourceContext");

                if (source != null && source.EndsWith(".LogicalHandler") && source.StartsWith("System.Net.Http.HttpClient."))
                {
                    // We want to filter out all logs from the SourceContexts "System.Net.Http.HttpClient.MyCustomHttpClient.LogicalHandler".
                    return log.Level < detailedLogsMinLevel;
                }

                return false;
            });

        // Add current configuration, allowing overriding of the custom minimum levels above through environment variables or appsettings.
        loggerConfiguration = loggerConfiguration
            .ReadFrom.Configuration(configuration);

        return loggerConfiguration.CreateLogger();
    }

    private static LogEventLevel? GetCurrentLoggingMinimumLevel(IConfiguration configuration)
    {
        return configuration.GetValue<LogEventLevel?>("Serilog:MinimumLevel") ?? configuration.GetValue<LogEventLevel?>("Serilog:MinimumLevel:Default");
    }

    private static T GetLogPropertyValue<T>(LogEvent log, string propertyName)
    {
        if (log.Properties.TryGetValue(propertyName, out var property) &&
            property is ScalarValue scalarProperty &&
            scalarProperty.Value is T propertyValue)
        {
            return propertyValue;
        }

        return default;
    }
}
