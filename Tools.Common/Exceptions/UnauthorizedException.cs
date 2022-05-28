namespace GoSolve.Tools.Common.Exceptions;

/// <summary>
/// Exception class for when a service or user is unauthorized.
/// </summary>
public class UnauthorizedException : AuthException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    public UnauthorizedException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public UnauthorizedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
