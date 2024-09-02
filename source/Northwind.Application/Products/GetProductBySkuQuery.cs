namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record GetProductBySkuQuery(Guid ProductSku) : IQuery<ErrorOr<CatalogProductDTO>>;