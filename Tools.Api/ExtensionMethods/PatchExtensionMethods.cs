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
}
