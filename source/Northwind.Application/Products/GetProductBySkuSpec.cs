namespace Northwind.Application.Products;

using Ardalis.Specification;
using Infra.Persistence.EF.Entities;

public sealed class GetProductBySkuSpec : Specification<Product>
{
  public GetProductBySkuSpec(Guid productSkuParam)
  {
    ProductSku = productSkuParam;
    Query.Where(product => product.Sku.Equals(ProductSku));
  }

  public Guid ProductSku { get; }
}