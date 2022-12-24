using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace GoSolve.Tools.Common.ExtensionMethods;

/// <summary>
/// Extension methods for JsonPatch.
/// </summary>
public static class PatchExtensionMethods
{
    /// <summary>
    /// Creates a normal, a JsonPatchDocument and a Operation mapping for the provided types.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <param name="source"></param>
    /// <param name="reverseMap">Whether reverseMap should be applied to all JsonPatch mappings. reverseMap will not be applied to the direct mapping of TSource -> TDestination.</param>
    /// <returns>The result of CreateMap&lt;TSource, TDestination&gt;</returns>
    public static IMappingExpression<TSource, TDestination> CreateJsonPatchMap<TSource, TDestination>(this Profile source, bool reverseMap = true)
        where TSource : class
        where TDestination : class
    {
        var docMapResult = source.CreateMap<JsonPatchDocument<TSource>, JsonPatchDocument<TDestination>>();
        var opMapResult = source.CreateMap<Operation<TSource>, Operation<TDestination>>();
        var typeMapResult = source.CreateMap<TSource, TDestination>();

        if (reverseMap)
        {
            docMapResult.ReverseMap();
            opMapResult.ReverseMap();
        }

        return typeMapResult;
    }
}
