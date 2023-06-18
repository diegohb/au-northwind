namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record GetAllProductsQuery : IQuery<ErrorOr<IList<CatalogProductDTO>>>;