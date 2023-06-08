namespace Northwind.Application.Products;

using Abstractions;
using Core.Persistence;
using Infra.Persistence.EF.Entities;

public class GetProductsHandler
  : IQueryHandler<GetAllProductsQuery, IList<CatalogProductDTO>>,
    IQueryHandler<GetProductBySku, CatalogProductDTO?>
{
  private readonly IQueryRepository<Product, int> _queryRepo;

  public GetProductsHandler(IQueryRepository<Product, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<IList<CatalogProductDTO>> Handle(GetAllProductsQuery requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntities = await _queryRepo.GetAllAsync();
    return productEntities.Select(getDTOFromEntity).ToList();
  }

  public async Task<CatalogProductDTO?> Handle(GetProductBySku requestParam, CancellationToken cancellationTokenParam)
  {
    var products = await _queryRepo.FindBySpecificationAsync(new GetProductBySkuSpec(requestParam.ProductId));
    var entity = products.SingleOrDefault();
    if (entity == null)
    {
      return null;
    }

    return getDTOFromEntity(entity);
  }

  #region Support Methods

  private CatalogProductDTO getDTOFromEntity(Product entityParam)
  {
    var dto = new CatalogProductDTO
    (CategoryName: entityParam.Category.CategoryName,
      Sku: entityParam.Sku,
      ProductId: entityParam.ProductId,
      ProductName: entityParam.ProductName,
      Description: entityParam.Description,
      UnitPrice: entityParam.UnitPrice,
      UnitsInStock: entityParam.UnitsInStock.GetValueOrDefault(0));

    return dto;
  }

  #endregion
}