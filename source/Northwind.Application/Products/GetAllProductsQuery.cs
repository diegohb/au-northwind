namespace Northwind.Application.Products;

using Abstractions;
using Infra.Persistence.EF.Entities;

public record GetAllProductsQuery : IQuery<IList<Product>>;

public record GetProductBySku(Guid ProductId) : IQuery<Product>;