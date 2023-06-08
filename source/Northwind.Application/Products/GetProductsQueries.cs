namespace Northwind.Application.Products;

using Abstractions;

public record GetAllProductsQuery : IQuery<IList<CatalogProductDTO>>;

public record GetProductBySkuQuery(Guid ProductId) : IQuery<CatalogProductDTO>;

public record GetProductsByCategoryQuery(string CategoryName) : IQuery<IList<CatalogProductDTO>>;