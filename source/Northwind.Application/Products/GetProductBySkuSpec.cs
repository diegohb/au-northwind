namespace Northwind.Application.Products;

using System.Linq.Expressions;
using Infra.Persistence.EF.Entities;
using SharedKernel.Specs;

public class GetProductBySkuSpec : Specification<Product>
{
  public GetProductBySkuSpec(Guid productSkuParam)
  {
    ProductSku = productSkuParam;
  }

  public Guid ProductSku { get; }

  public override Expression<Func<Product, bool>> SatisfiedBy()
  {
    return product => product.Sku.Equals(ProductSku);
  }
}