namespace GoSolve.Tools.Common.Exceptions;

/// <summary>
/// Exception class for when a configuration setting is missing or invalid.
/// </summary>
public class ConfigurationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
    /// </summary>
    public ConfigurationException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public ConfigurationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ConfigurationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
