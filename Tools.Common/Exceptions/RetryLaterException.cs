namespace GoSolve.Tools.Common.Exceptions;

/// <summary>
/// Exception thrown for actions that should be retried.
/// </summary>
public abstract class RetryLaterException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RetryLaterException"/> class.
    /// </summary>
    public RetryLaterException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryLaterException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public RetryLaterException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RetryLaterException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public RetryLaterException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
