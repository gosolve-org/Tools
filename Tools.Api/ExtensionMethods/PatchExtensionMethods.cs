using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace GoSolve.Tools.Api.ExtensionMethods;

/// <summary>
/// Extension methods for JsonPatch.
/// </summary>
public static class PatchExtensionMethods
{
    /// <summary>
    /// Applies the JsonPatchDocument to the provided model and validates that model.
    /// </summary>
    /// <typeparam name="T">Type of the patch request contract.</typeparam>
    /// <param name="patchDoc"></param>
    /// <param name="controller">The current controller.</param>
    /// <param name="actualModel">The fetched model with all current data.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Thrown when actualModel is null.</exception>
    public static bool ApplyAndValidate<T>(
        this JsonPatchDocument<T> patchDoc,
        ControllerBase controller,
        T actualModel)
            where T : class
    {
        if (actualModel == null) throw new Exception("actualModel can not be null.");
        patchDoc.ApplyTo(actualModel, controller.ModelState);
        return controller.TryValidateModel(actualModel);
    }

    /// <summary>
    /// Creates a normal, a JsonPatchDocument and a Operation mapping for the provided types.
    /// </summary>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <param name="source"></param>
    /// <param name="reverseMap">Whether reverseMap should be applied to all mappings.</param>
    public static void CreateJsonPatchMap<TSource, TDestination>(this Profile source, bool reverseMap = true)
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
            typeMapResult.ReverseMap();
        }
    }
}
