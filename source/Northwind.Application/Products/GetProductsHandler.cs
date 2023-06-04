namespace Northwind.Application.Products;

using Abstractions;
using Core.Persistence;
using Infra.Persistence.EF.Entities;

public class GetProductsHandler
  : IQueryHandler<GetAllProductsQuery, IList<Product>>,
    IQueryHandler<GetProductBySku, Product?>
{
  private readonly IQueryRepository<Product, int> _queryRepo;

  public GetProductsHandler(IQueryRepository<Product, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<IList<Product>> Handle(GetAllProductsQuery requestParam, CancellationToken cancellationTokenParam)
  {
    var productEntities = await _queryRepo.GetAllAsync();
    return productEntities.ToList();
  }

  public async Task<Product?> Handle(GetProductBySku requestParam, CancellationToken cancellationTokenParam)
  {
    var products = await _queryRepo.FindBySpecificationAsync(new GetProductBySkuSpec(requestParam.ProductId));
    return products.SingleOrDefault();
  }
}