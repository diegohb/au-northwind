namespace Infra.Persistence.EF;

using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class GenericQueryRepository<TEntity, TEntityID> : RepositoryBase<TEntity>
  where TEntity : class
  where TEntityID : notnull
{
  protected readonly NorthwindDbContext _context;

  public GenericQueryRepository(NorthwindDbContext contextParam)
    : base(contextParam)
  {
    _context = contextParam;
    _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    _context.ChangeTracker.AutoDetectChangesEnabled = false;
  }
}