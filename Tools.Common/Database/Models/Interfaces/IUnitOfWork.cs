namespace GoSolve.Tools.Common.Database.Models.Interfaces;

/// <summary>
/// Interface used to group one or more operations into a single transaction or "unit of work".
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Completes the transaction.
    /// </summary>
    /// <returns></returns>
    Task CompleteAsync();
}
