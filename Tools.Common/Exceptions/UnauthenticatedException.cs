namespace GoSolve.Tools.Common.Exceptions;

/// <summary>
/// Exception class for when a service or user is unauthenticated.
/// </summary>
public class UnauthenticatedException : AuthException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedException"/> class.
    /// </summary>
    public UnauthenticatedException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public UnauthenticatedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthenticatedException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public UnauthenticatedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
