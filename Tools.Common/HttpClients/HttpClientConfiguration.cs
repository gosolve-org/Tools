namespace GoSolve.Tools.Common.HttpClients;

/// <summary>
/// Configuration class for a registered HttpClient.
/// </summary>
public class HttpClientConfiguration
{
    /// <summary>
    /// Name of the HttpClient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Base address uri of the HttpClient.
    /// </summary>
    public string BaseAddressUri { get; set; }

    /// <summary>
    /// The content type the HttpClient accepts.
    /// </summary>
    public string Accept { get; set; } = "application/json";
}
