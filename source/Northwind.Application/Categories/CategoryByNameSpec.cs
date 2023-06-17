namespace Northwind.Application.Categories;

using Ardalis.Specification;
using Infra.Persistence.EF.Entities;

public sealed class CategoryByNameSpec : Specification<Category>
{
  public CategoryByNameSpec(string nameParam)
  {
    Name = nameParam;
    Query.Where(p => p.CategoryName.Equals(nameParam));
  }

  public string Name { get; }
}