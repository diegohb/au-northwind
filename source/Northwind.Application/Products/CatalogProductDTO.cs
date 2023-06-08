namespace Northwind.Application.Products;

public record CatalogProductDTO
  (int ProductId, Guid Sku, string CategoryName, string ProductName, string Description, decimal? UnitPrice, int UnitsInStock);