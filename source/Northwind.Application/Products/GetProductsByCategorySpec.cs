namespace Northwind.Application.Products;

using Ardalis.Specification;
using Infra.Persistence.EF.Entities;

public sealed class GetProductsByCategorySpec : Specification<Product>
{
  public GetProductsByCategorySpec(string categoryNameParam)
  {
    CategoryName = categoryNameParam;

    Query.Where(entity => entity.Category.CategoryName.Equals(categoryNameParam));
  }

  public string CategoryName { get; }
}