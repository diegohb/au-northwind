namespace Northwind.Application.Categories;

using Abstractions;
using ErrorOr;
using Infra.Persistence.EF;
using Infra.Persistence.EF.Entities;

public class GetCategoriesHandler
  : IQueryHandler<GetCategoriesQuery, ErrorOr<IList<CatalogCategoryDTO>>>,
    IQueryHandler<GetCategoryByIDQuery, ErrorOr<CatalogCategoryDTO>>,
    IQueryHandler<GetCategoryByNameQuery, ErrorOr<CatalogCategoryDTO>>
{
  private readonly GenericQueryRepository<Category, int> _queryRepo;

  public GetCategoriesHandler(GenericQueryRepository<Category, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<ErrorOr<IList<CatalogCategoryDTO>>> Handle(GetCategoriesQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var activeCategories = await _queryRepo.ListAsync(cancellationTokenParam);
    if (!activeCategories.Any())
    {
      return Error.NotFound();
    }

    return activeCategories.Select(getDTOFromEntity).ToList();
  }

  public async Task<ErrorOr<CatalogCategoryDTO>> Handle(GetCategoryByIDQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categoryEntity = await _queryRepo.GetByIdAsync(queryParam.CategoryID, cancellationTokenParam);
    if (categoryEntity == null)
    {
      return Error.NotFound();
    }

    return getDTOFromEntity(categoryEntity);
  }

  public async Task<ErrorOr<CatalogCategoryDTO>> Handle(GetCategoryByNameQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categories = await _queryRepo.ListAsync(new CategoryByNameSpec(queryParam.Name), cancellationTokenParam);
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