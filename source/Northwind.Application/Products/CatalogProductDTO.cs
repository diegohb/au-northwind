namespace Northwind.Application.Products;

public record CatalogProductDTO(int ProductId, Guid Sku, string ProductName, string Description, decimal? UnitPrice, int UnitsInStock);