using Hellang.Middleware.ProblemDetails;

namespace GoSolve.Tools.Api.ProblemDetails
{
    /// <summary>
    /// Proxy for the class Hellang.Middleware.ProblemDetails.ProblemDetailsOptions.
    /// </summary>
    public class ProblemDetailsOptionsProxy : IProblemDetailsOptions
    {
        private readonly ProblemDetailsOptions _problemDetailsOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsOptionsProxy"/> class.
        /// </summary>
        /// <param name="problemDetailsOptions"></param>
        public ProblemDetailsOptionsProxy(ProblemDetailsOptions problemDetailsOptions)
        {
            _problemDetailsOptions = problemDetailsOptions;
        }

        /// <summary>
        /// Maps the specified exception type <typeparamref name="TException" /> to a <see cref="T:Microsoft.AspNetCore.Mvc.ProblemDetails" /> instance
        /// using the specified <paramref name="mapping" /> function.
        /// </summary>
        /// <remarks>
        /// Mappers are called in the order they're registered.
        /// Returning <c>null</c> from the mapper will signify that you can't or don't want to map the exception to <see cref="T:Microsoft.AspNetCore.Mvc.ProblemDetails" />.
        /// This will cause the exception to be rethrown.
        /// </remarks>
        /// <param name="mapping">The mapping function for creating a problem details instance.</param>
        /// <typeparam name="TException">The exception type to map using the specified mapping function.</typeparam>
        public void Map<TException>(Func<TException, Microsoft.AspNetCore.Mvc.ProblemDetails> mapping)
            where TException : Exception
        {
            _problemDetailsOptions.Map(mapping);
        }
    }
}
