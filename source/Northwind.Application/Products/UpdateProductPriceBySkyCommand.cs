namespace Northwind.Application.Products;

using Abstractions;
using ErrorOr;

public record UpdateProductPriceBySkyCommand(Guid ProductSku, decimal OriginalPrice, decimal ChangeAmount, string Comment) : ICommand<ErrorOr<bool>>;