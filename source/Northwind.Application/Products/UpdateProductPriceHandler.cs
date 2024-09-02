namespace Northwind.Application.Products;

using Abstractions;
using Core.Persistence;
using Domain.Catalog.Product;
using ErrorOr;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;

public class UpdateProductPriceHandler
  : ICommandHandler<UpdateProductPriceBySkuCommand, ErrorOr<bool>>
{
  private readonly IAggregateRepository<CatalogProduct, ProductId> _aggregateRepo;
  private readonly GenericQueryRepository<Product, int> _productRepo;

  public UpdateProductPriceHandler
    (GenericQueryRepository<Product, int> productRepoParam, IAggregateRepository<CatalogProduct, ProductId> aggregateRepoParam)
  {
    _productRepo = productRepoParam;
    _aggregateRepo = aggregateRepoParam;
  }

  public async Task<ErrorOr<bool>> Handle(UpdateProductPriceBySkuCommand requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntity = await _productRepo.SingleOrDefaultAsync(new GetProductBySkuSpec(requestParam.ProductSku), cancellationTokenParam);
    if (productEntity is null)
    {
      return Error.NotFound();
    }

    if (requestParam.ChangeAmount.Equals(0))
    {
      return Error.Validation(description: "Cannot change price by zero.");
    }

    var productAgg = await _aggregateRepo.GetByIdAsync(ProductId.NewProductId(productEntity.ProductId));
    if (productAgg is null)
    {
      return Error.NotFound();
    }

    if (!productAgg.Price.Equals(requestParam.OriginalPrice))
    {
      return Error.Conflict();
    }

    try
    {
      if (requestParam.ChangeAmount < 0)
      {
        productAgg.IncreaseListPrice(requestParam.ChangeAmount, requestParam.Comment);
      }
      else
      {
        productAgg.DecreaseListPrice(requestParam.ChangeAmount, requestParam.Comment);
      }
    }
    catch (Exception exception)
    {
      return Error.Failure(description: exception.Message);
    }

    return ErrorOrFactory.From(true);
  }
}