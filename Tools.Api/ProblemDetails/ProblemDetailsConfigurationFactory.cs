using GoSolve.Tools.Api.ProblemDetails.Interfaces;
using GoSolve.Tools.Common.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace GoSolve.Tools.Api.ProblemDetails;

/// <summary>
/// Factory for creating ProblemDetails configurations.
/// </summary>
public static class ProblemDetailsConfigurationFactory
{
    /// <summary>
    /// Creates the default ProblemDetails configuration.
    /// </summary>
    public static readonly Action<IProblemDetailsOptions> CreateDefaultProblemDetailsOptionsConfiguration =
        (IProblemDetailsOptions options) =>
        {
            static Microsoft.AspNetCore.Mvc.ProblemDetails AddDetailToProblemDetails(
                Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails, Exception ex)
            {
                problemDetails.Detail = ex.Message;
                return problemDetails;
            }

            options.Map<ArgumentException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status400BadRequest), ex));
            options.Map<NotFoundException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound), ex));
            options.Map<InvalidOperationException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status400BadRequest), ex));
            options.Map<NotImplementedException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status501NotImplemented), ex));
            options.Map<UnauthenticatedException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status401Unauthorized), ex));
            options.Map<UnauthorizedException>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status403Forbidden), ex));
            options.Map<Exception>(ex =>
                AddDetailToProblemDetails(StatusCodeProblemDetails.Create(StatusCodes.Status500InternalServerError), ex));
        };
}
