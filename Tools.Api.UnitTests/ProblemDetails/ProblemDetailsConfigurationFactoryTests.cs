using Moq;
using Xunit;
using GoSolve.Tools.Api.ProblemDetails;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using GoSolve.Tools.Common.Exceptions;

namespace GoSolve.Tools.Api.UnitTests.ProblemDetails;

public class ProblemDetailsConfigurationFactoryTests
{
    private readonly Mock<IProblemDetailsOptions> _problemDetailsOptionsMock;

    public ProblemDetailsConfigurationFactoryTests()
    {
        _problemDetailsOptionsMock = new Mock<IProblemDetailsOptions>(MockBehavior.Strict);
        var mockSequence = new MockSequence();

        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<ArgumentException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<NotFoundException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<InvalidOperationException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<NotImplementedException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<UnauthenticatedException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<UnauthorizedException, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
        _problemDetailsOptionsMock.InSequence(mockSequence).Setup(x => x.Map(It.IsAny<Func<Exception, Microsoft.AspNetCore.Mvc.ProblemDetails>>()));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsArgumentExceptionToBadRequest()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<ArgumentException>(StatusCodes.Status400BadRequest));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsNotFoundExceptionToNotFound()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<NotFoundException>(StatusCodes.Status404NotFound));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsInvalidOperationExceptionToBadRequest()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<InvalidOperationException>(StatusCodes.Status400BadRequest));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsNotImplementedExceptionToNotImplemented()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<NotImplementedException>(StatusCodes.Status501NotImplemented));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsUnauthenticatedExceptionToUnauthorized()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<UnauthenticatedException>(StatusCodes.Status401Unauthorized));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsUnauthorizedExceptionToForbidden()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<UnauthorizedException>(StatusCodes.Status403Forbidden));
    }

    [Fact]
    public void CreateDefaultProblemDetailsOptionsConfiguration_MapsExceptionToInternalServerError()
    {
        ProblemDetailsConfigurationFactory.CreateDefaultProblemDetailsOptionsConfiguration(_problemDetailsOptionsMock.Object);

        _problemDetailsOptionsMock.Verify(VerifyExceptionMapping<Exception>(StatusCodes.Status500InternalServerError));
    }

    private static Expression<Action<IProblemDetailsOptions>> VerifyExceptionMapping<TException>(int statusCode)
        where TException : Exception, new()
    {
        return el => el.Map(It.Is<Func<TException, Microsoft.AspNetCore.Mvc.ProblemDetails>>(cb => cb(new TException()).Status == statusCode));
    }
}
