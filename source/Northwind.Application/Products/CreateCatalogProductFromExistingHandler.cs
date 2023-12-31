namespace Northwind.Application.Products;

using Abstractions;
using Core.Persistence;
using Domain.Catalog.Category;
using Domain.Catalog.Product;
using ErrorOr;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;

public class CreateCatalogProductFromExistingHandler : ICommandHandler<CreateCatalogProductFromExistingCommand, ErrorOr<CatalogProductDTO>>
{
  private readonly IAggregateRepository<CatalogProduct, ProductId> _aggregateRepo;
  private readonly GenericQueryRepository<Product, int> _productRepo;

  public CreateCatalogProductFromExistingHandler
    (GenericQueryRepository<Product, int> productRepoParam, IAggregateRepository<CatalogProduct, ProductId> aggregateRepoParam)
  {
    _productRepo = productRepoParam;
    _aggregateRepo = aggregateRepoParam;
  }

  public async Task<ErrorOr<CatalogProductDTO>> Handle(CreateCatalogProductFromExistingCommand requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntity = await _productRepo.SingleOrDefaultAsync(new GetProductBySkuSpec(requestParam.ProductSku), cancellationTokenParam);
    if (productEntity is null)
    {
      return Error.NotFound();
    }

    var productAggregate = new CatalogProduct(ProductId.NewProductId(productEntity.ProductId), requestParam.ProductSku);
    productAggregate.Rename(productEntity.ProductName);
    productAggregate.DescribeProduct(productEntity.Description);
    productAggregate.IncreaseListPrice(productEntity.UnitPrice.GetValueOrDefault(0), "price from mssql");

    if (productEntity.CategoryId != null)
    {
      productAggregate.Categorize(CategoryId.NewCategoryId(productEntity.CategoryId.Value));
    }

    await _aggregateRepo.SaveAsync(productAggregate);

    return ErrorOr.From
    (new CatalogProductDTO
    (productEntity.ProductId, productAggregate.Sku, productEntity.Category.CategoryName, productAggregate.Name!, productAggregate.Description!,
      productAggregate.Price, 0));
  }
}

public record CreateCatalogProductFromExistingCommand(Guid ProductSku) : ICommand<ErrorOr<CatalogProductDTO>>;