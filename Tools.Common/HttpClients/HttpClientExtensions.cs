using GoSolve.Tools.Common.Enumerations;
using GoSolve.Tools.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoSolve.Tools.Common.HttpClients;

/// <summary>
/// Extension methods for HttpClients.
/// </summary>
public static class HttpClientExtensions
{
    private const string ApplicationNameConfigurationProperty = "ApplicationName";
    private const string HttpClientsConfigurationProperty = "HttpClients";

    /// <summary>
    /// Adds a HttpClient for an internal goSolve api.
    /// </summary>
    /// <typeparam name="TInterface">Interface of the HttpClient.</typeparam>
    /// <typeparam name="TClass">HttpClient class that inherits TInterface.</typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="name">The name of the HttpClient, defined in your appsettings.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws Exception when the needed configuration variables are not set.</exception>
    public static IHttpClientBuilder AddInternalHttpClient<TInterface, TClass>(
        this IServiceCollection services,
        IConfiguration configuration,
        string name)
            where TInterface : class
            where TClass : class, TInterface
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ConfigurationException("HttpClient name can not be empty.");
        var httpClientConfiguration = GetHttpClientConfiguration(configuration, name);
        if (string.IsNullOrWhiteSpace(httpClientConfiguration.BaseAddressUri))
            throw new ConfigurationException($"{nameof(httpClientConfiguration.BaseAddressUri)} for HttpClient {name} cannot be empty.");

        var builder = services
            .AddHttpClient<TInterface, TClass>(typeof(TClass).Name, client =>
            {
                client.BaseAddress = new Uri(EnsureTrailingSlash(httpClientConfiguration.BaseAddressUri));
                client.DefaultRequestHeaders.Add("Accept", httpClientConfiguration.Accept);
                client.DefaultRequestHeaders.Add("User-Agent", GetCurrentApplicationName(configuration));
                client.DefaultRequestVersion = new Version(2, 0);
            })
            .AddPolicyHandler(HttpPolicyFactory.CreateRetryPolicy());

        return builder;
    }

    private static string EnsureTrailingSlash(string uri)
    {
        return uri.EndsWith('/') ? uri : uri + "/";
    }

    private static string GetCurrentApplicationName(IConfiguration configuration)
    {
        var applicationName = configuration.GetSection(ApplicationNameConfigurationProperty)?.Value;

        if (string.IsNullOrWhiteSpace(applicationName))
        {
            throw new ConfigurationException("The current application does not have the configuration variable " +
                $"{ApplicationNameConfigurationProperty} set.");
        }

        return applicationName;
    }

    private static HttpClientConfiguration GetHttpClientConfiguration(IConfiguration configuration, string name)
    {
        var httpClients = new List<HttpClientConfiguration>();
        configuration.Bind(HttpClientsConfigurationProperty, httpClients);
        var httpClient = httpClients?.Find(httpClient => httpClient.Name == name);
        if (httpClient == null)
        {
            throw new ConfigurationException($"Could not find HttpClient in configuration with Name: {name}.");
        }

        return httpClient;
    }
}
