namespace Northwind.Application.Products;

using Abstractions;
using Core.Persistence;
using ErrorOr;
using Infra.Persistence.EF.Entities;
using SharedKernel.Specs;

public class GetProductsHandler
  : IQueryHandler<GetAllProductsQuery, ErrorOr<IList<CatalogProductDTO>>>,
    IQueryHandler<GetProductsByCategoryQuery, ErrorOr<IList<CatalogProductDTO>>>,
    IQueryHandler<GetProductBySkuQuery, ErrorOr<CatalogProductDTO>>
{
  private readonly IQueryRepository<Product, int> _queryRepo;

  public GetProductsHandler(IQueryRepository<Product, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<ErrorOr<IList<CatalogProductDTO>>> Handle(GetAllProductsQuery requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntities = await _queryRepo.GetAllAsync();
    return productEntities.Select(getDTOFromEntity).ToList();
  }

  public async Task<ErrorOr<CatalogProductDTO>> Handle(GetProductBySkuQuery requestParam, CancellationToken cancellationTokenParam)
  {
    var products = await _queryRepo.FindBySpecificationAsync(new GetProductBySkuSpec(requestParam.ProductId));
    var entity = products.SingleOrDefault();
    if (entity == null)
    {
      return Error.NotFound();
    }

    return getDTOFromEntity(entity);
  }

  public async Task<ErrorOr<IList<CatalogProductDTO>>> Handle(GetProductsByCategoryQuery requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntities = await _queryRepo.FindBySpecificationAsync
      (new DirectSpecification<Product>(entity => entity.Category.CategoryName.Equals(requestParam.CategoryName)));
    return productEntities.Select(getDTOFromEntity).ToList();
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