using GoSolve.Tools.Common.Globals;

namespace GoSolve.Tools.Common.Helpers;

/// <summary>
/// Helper for the current environment.
/// </summary>
public static class EnvironmentHelper
{
    /// <summary>
    /// Checks whether the current environment is a development environment.
    /// </summary>
    /// <returns></returns>
    public static bool IsDevelopment()
    {
        return string.Equals(
            Environment.GetEnvironmentVariable(Constants.ENVIRONMENT_VARIABLE),
            Constants.DEVELOPMENT_ENVIRONMENT,
            StringComparison.OrdinalIgnoreCase);
    }
}
