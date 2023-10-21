namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record UpdateProductPriceBySkuCommand(Guid ProductSku, decimal OriginalPrice, decimal ChangeAmount, string Comment) : ICommand<ErrorOr<bool>>;