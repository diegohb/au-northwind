namespace Infra.Persistence.EF;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Core.Persistence;
using SharedKernel.Specs;

public class GenericQueryRepository<TEntity, TEntityID> : IQueryRepository<TEntity, TEntityID>
  where TEntity : class
{
  protected readonly NorthwindDbContext _context;
  protected readonly DbSet<TEntity> _table;

  public GenericQueryRepository(NorthwindDbContext contextParam)
  {
    _context = contextParam;
    _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
    _context.ChangeTracker.AutoDetectChangesEnabled = false;

    _table = _context.Set<TEntity>();
  }

  public bool Contains(Expression<Func<TEntity, bool>> predicateParam)
  {
    return _table.Any(predicateParam);
  }

  public bool Contains(ISpecification<TEntity> specificationParam)
  {
    return _table.Any(specificationParam.SatisfiedBy());
  }

  public async Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicateParam)
  {
    return await _table.AnyAsync(predicateParam);
  }

  public async Task<bool> ContainsAsync(ISpecification<TEntity> specificationParam)
  {
    return await _table.AnyAsync(specificationParam.SatisfiedBy());
  }

  public int Count()
  {
    return _table.Count();
  }

  public int Count(Expression<Func<TEntity, bool>> predicateParam)
  {
    return _table.Count(predicateParam);
  }

  public int Count(ISpecification<TEntity> specificationParam)
  {
    return _table.Count(specificationParam.SatisfiedBy());
  }

  public async Task<int> CountAsync()
  {
    return await _table.CountAsync();
  }

  public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicateParam)
  {
    return await _table.CountAsync(predicateParam);
  }

  public async Task<int> CountAsync(ISpecification<TEntity> specificationParam)
  {
    return await _table.CountAsync(specificationParam.SatisfiedBy());
  }

  public IQueryable<TEntity> FindBySpecification(ISpecification<TEntity> specificationParam)
  {
    return _table.Where(specificationParam.SatisfiedBy());
  }

  public async Task<IEnumerable<TEntity>> FindBySpecificationAsync(ISpecification<TEntity> specificationParam)
  {
    return await _table.Where(specificationParam.SatisfiedBy()).ToListAsync();
  }

  public IQueryable<TEntity> GetAll()
  {
    return _table;
  }

  public async Task<IEnumerable<TEntity>> GetAllAsync()
  {
    return await _table.ToListAsync();
  }

  public TEntity GetById(TEntityID entityIdParam)
  {
    return _table.Find(entityIdParam);
  }

  public async Task<TEntity> GetByIdAsync(TEntityID entityIdParam)
  {
    return await _table.FindAsync(entityIdParam);
  }
}