namespace Northwind.Application.Products;

using Abstractions;

public record GetAllProductsQuery : IQuery<IList<CatalogProductDTO>>;

public record GetProductBySku(Guid ProductId) : IQuery<CatalogProductDTO>;