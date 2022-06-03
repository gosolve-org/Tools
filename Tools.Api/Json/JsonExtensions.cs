using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GoSolve.Tools.Api.Json;

/// <summary>
/// Class containing json extension methods.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Configures the settings for Newtonsoft.Json functionality.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder builder)
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter(),
                new IsoDateTimeConverter()
            }
        };

        return builder.AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            opt.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
        });
    }
}
