namespace Northwind.Core.Persistence;

using System.Linq.Expressions;
using SharedKernel.Specs;

public interface IQueryRepository<TEntity, in TEntityID>
  where TEntity : class
{
  bool Contains(ISpecification<TEntity> specificationParam);

  bool Contains(Expression<Func<TEntity, bool>> predicateParam);

  Task<bool> ContainsAsync(ISpecification<TEntity> specificationParam);

  Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicateParam);

  int Count(ISpecification<TEntity> specificationParam);

  int Count(Expression<Func<TEntity, bool>> predicateParam);

  Task<int> CountAsync(ISpecification<TEntity> specificationParam);

  Task<int> CountAsync(Expression<Func<TEntity, bool>> predicateParam);

  IQueryable<TEntity> FindBySpecification(ISpecification<TEntity> specificationParam);

  Task<IQueryable<TEntity>> FindBySpecificationAsync(ISpecification<TEntity> specificationParam);

  IQueryable<TEntity> GetAll();

  Task<IQueryable<TEntity>> GetAllAsync();

  TEntity GetById(TEntityID entityIdParam);

  Task<TEntity> GetByIdAsync(TEntityID entityIdParam);
}