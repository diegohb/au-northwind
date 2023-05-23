namespace Northwind.Application.Categories;

using Abstractions;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;

public class GetCategoriesHandler
  : IQueryHandler<GetCategoriesQuery, IList<Category>>,
    IQueryHandler<GetCategoryByIDQuery, Category?>,
    IQueryHandler<GetCategoryByNameQuery, Category?>
{
  private readonly GenericQueryRepository<Category, int> _queryRepo;

  public GetCategoriesHandler(GenericQueryRepository<Category, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<IList<Category>> Handle(GetCategoriesQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var activeCategories = await _queryRepo.GetAllAsync();
    return activeCategories.ToList();
  }

  public async Task<Category?> Handle(GetCategoryByIDQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categoryEntity = await _queryRepo.GetByIdAsync(queryParam.CategoryID);
    return categoryEntity;
  }

  public async Task<Category?> Handle(GetCategoryByNameQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categories = (await _queryRepo.FindBySpecificationAsync(new CategoryByNameSpec(queryParam.Name))).ToList();
    if (categories.Count > 1)
    {
      throw new MoreThan1CategoryByNameException(queryParam.Name, categories.Count);
    }

    return categories.SingleOrDefault();
  }
}