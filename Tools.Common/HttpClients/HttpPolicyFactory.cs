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
}
