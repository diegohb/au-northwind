namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record GetProductsByCategoryQuery(string CategoryName) : IQuery<ErrorOr<IList<CatalogProductDTO>>>;