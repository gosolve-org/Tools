using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace GoSolve.Tools.Api.ProblemDetails.Interfaces;

/// <summary>
/// Interface for the class Hellang.Middleware.ProblemDetails.ProblemDetailsOptions.
/// </summary>
public interface IProblemDetailsOptions
{
    /// <summary>
    /// Maps the specified exception type <typeparamref name="TException"/> to a <see cref="MvcProblemDetails"/> instance
    /// using the specified <paramref name="mapping"/> function.
    /// </summary>
    /// <remarks>
    /// Mappers are called in the order they're registered.
    /// Returning <c>null</c> from the mapper will signify that you can't or don't want to map the exception to <see cref="MvcProblemDetails"/>.
    /// This will cause the exception to be rethrown.
    /// </remarks>
    /// <param name="mapping">The mapping function for creating a problem details instance.</param>
    /// <typeparam name="TException">The exception type to map using the specified mapping function.</typeparam>
    public void Map<TException>(Func<TException, MvcProblemDetails> mapping)
        where TException : Exception;
}
