namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record GetAllProductsQuery : IQuery<ErrorOr<IList<CatalogProductDTO>>>;

public record GetProductBySkuQuery(Guid ProductId) : IQuery<ErrorOr<CatalogProductDTO>>;

public record GetProductsByCategoryQuery(string CategoryName) : IQuery<ErrorOr<IList<CatalogProductDTO>>>;