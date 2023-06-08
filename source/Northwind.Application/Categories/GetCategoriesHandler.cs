namespace Northwind.Application.Categories;

using Abstractions;
using Core.Persistence;
using ErrorOr;
using Infra.Persistence.EF.Entities;

public class GetCategoriesHandler
  : IQueryHandler<GetCategoriesQuery, ErrorOr<IList<CatalogCategoryDTO>>>,
    IQueryHandler<GetCategoryByIDQuery, ErrorOr<CatalogCategoryDTO>>,
    IQueryHandler<GetCategoryByNameQuery, ErrorOr<CatalogCategoryDTO>>
{
  private readonly IQueryRepository<Category, int> _queryRepo;

  public GetCategoriesHandler(IQueryRepository<Category, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<ErrorOr<IList<CatalogCategoryDTO>>> Handle(GetCategoriesQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var activeCategories = await _queryRepo.GetAllAsync();
    var categories = activeCategories.ToList();
    if (!categories.Any())
    {
      return Error.NotFound();
    }

    return categories.Select(getDTOFromEntity).ToList();
  }

  public async Task<ErrorOr<CatalogCategoryDTO>> Handle(GetCategoryByIDQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categoryEntity = await _queryRepo.GetByIdAsync(queryParam.CategoryID);
    if (categoryEntity == null)
    {
      return Error.NotFound();
    }

    return getDTOFromEntity(categoryEntity);
  }

  public async Task<ErrorOr<CatalogCategoryDTO>> Handle(GetCategoryByNameQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categories = (await _queryRepo.FindBySpecificationAsync(new CategoryByNameSpec(queryParam.Name))).ToList();
    if (categories.Count > 1)
    {
      return Error.Unexpected(description: $"More than 1 category found by the name '{queryParam.Name}'.");
    }

    var categoryEntity = categories.SingleOrDefault();
    return categoryEntity == null
      ? Error.NotFound()
      : getDTOFromEntity(categoryEntity);
  }

  #region Support Methods

  private CatalogCategoryDTO getDTOFromEntity(Category entityParam)
  {
    return new CatalogCategoryDTO(entityParam.CategoryId, entityParam.CategoryName, entityParam.Description, entityParam.Products.Count);
  }

  #endregion
}