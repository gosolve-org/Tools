using GoSolve.Tools.Common.HttpClients.Models;
using Polly;
using Polly.Extensions.Http;
using Serilog;

namespace GoSolve.Tools.Common.HttpClients;

/// <summary>
/// Factory for creating Http policies.
/// </summary>
public static class HttpPolicyFactory
{
    private static readonly ILogger _logger = Log.ForContext(typeof(HttpPolicyFactory));

    /// <summary>
    /// Creates a retry policy with an exponential backoff.
    /// </summary>
    /// <returns></returns>
    public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4)
            }, (result, timespan) =>
            {
                if (result == null) return;

                _logger.Information("Http action failed, retrying. {@ExceptionMessage} {@StatusCode} {@RequestUri} {@TimeSpan}",
                    result.Exception?.Message, result.Result?.StatusCode, result.Result?.RequestMessage?.RequestUri, timespan);
            });
    }

    /// <summary>
    /// Creates a circuit breaker policy.
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IAsyncPolicy<HttpResponseMessage> CreateCircuitBreakerPolicy(CircuitBreakerConfiguration configuration)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .AdvancedCircuitBreakerAsync(
                failureThreshold: configuration.FailureThreshold,
                samplingDuration: TimeSpan.FromSeconds(configuration.SamplingDuration),
                minimumThroughput: configuration.MinimumThroughput,
                durationOfBreak: TimeSpan.FromSeconds(configuration.DurationOfBreak),
                onBreak: async (exception, breakDelay) =>
                {
                    if (exception != null && exception.Exception != null)
                    {
                        Log.Logger.Error("Action failed. Circuit breaker activated. {@BreakDelay} {@ExceptionMessage}",
                            breakDelay, exception.Exception);
                    }

                    if (exception != null && exception.Result != null)
                    {
                        var response = exception.Result;
                        var responseMessage = await response.Content.ReadAsStringAsync();
                        Log.Logger.Error("Action failed. Circuit breaker activated. {@BreakDelay} {@HttpResponseMessage}",
                            breakDelay, new { response.StatusCode, response.Version, ResponseMessageContent = responseMessage });
                    }
                },
                onReset: () =>
                {
                    Log.Logger.Information("Circuit breaker reset.");
                });
    }
}
