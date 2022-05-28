namespace GoSolve.Tools.Common.Exceptions;

/// <summary>
/// Base class for auth exceptions.
/// </summary>
public abstract class AuthException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthException"/> class.
    /// </summary>
    public AuthException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public AuthException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public AuthException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
