namespace GoSolve.Tools.Common.Globals;

/// <summary>
/// State that can be used and configured during the startup of the application.
/// </summary>
public static class StartupState
{
    /// <summary>
    /// Whether the database was newly created on startup.
    /// </summary>
    public static bool? DatabaseCreatedOnStartup { get; set; }

    /// <summary>
    /// Whether the database has been migrated (or checked if migrations were needed) on startup.
    /// </summary>
    public static bool DatabaseMigratedOnStartup { get; set; } = false;
}
