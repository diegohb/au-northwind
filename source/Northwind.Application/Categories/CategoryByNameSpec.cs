namespace Northwind.Application.Categories;

using System.Linq.Expressions;
using Infra.Persistence.EF.Entities;
using SharedKernel.Specs;

public sealed class CategoryByNameSpec : Specification<Category>
{
  private readonly string _name;

  public CategoryByNameSpec(string nameParam)
  {
    _name = nameParam;
  }

  public override Expression<Func<Category, bool>> SatisfiedBy()
  {
    return p => p.CategoryName.Equals(_name);
  }
}