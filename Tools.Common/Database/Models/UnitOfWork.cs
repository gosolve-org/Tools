using GoSolve.Tools.Common.Database.Models.Interfaces;

namespace GoSolve.Tools.Common.Database.Models;

/// <summary>
/// Class used to group one or more operations into a single transaction or "unit of work".
/// </summary>
/// <typeparam name="TDbContext">Type of the DbContext that is used for the entity operations in this unit of work.</typeparam>
public class UnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : BaseDbContext<TDbContext>
{
    private readonly TDbContext _context;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context"></param>
    public UnitOfWork(TDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Completes all operations in this unit of work.
    /// </summary>
    /// <returns></returns>
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disposes of the entire unit of work.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}
