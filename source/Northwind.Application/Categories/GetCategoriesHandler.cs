﻿namespace Northwind.Application.Categories;

using Abstractions;
using Core.Persistence;
using Infra.Persistence.EF.Entities;

public class GetCategoriesHandler
  : IQueryHandler<GetCategoriesQuery, IList<CatalogCategoryDTO>>,
    IQueryHandler<GetCategoryByIDQuery, CatalogCategoryDTO?>,
    IQueryHandler<GetCategoryByNameQuery, CatalogCategoryDTO?>
{
  private readonly IQueryRepository<Category, int> _queryRepo;

  public GetCategoriesHandler(IQueryRepository<Category, int> queryRepositoryParam)
  {
    _queryRepo = queryRepositoryParam;
  }

  public async Task<IList<CatalogCategoryDTO>> Handle(GetCategoriesQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var activeCategories = await _queryRepo.GetAllAsync();
    return activeCategories.Select
      (item => new CatalogCategoryDTO
      (
        item.CategoryId,
        item.CategoryName,
        item.Description,
        item.Products.Count
      ))
      .ToList();
  }

  public async Task<CatalogCategoryDTO?> Handle(GetCategoryByIDQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categoryEntity = await _queryRepo.GetByIdAsync(queryParam.CategoryID);
    return new CatalogCategoryDTO(categoryEntity.CategoryId, categoryEntity.CategoryName, categoryEntity.Description, categoryEntity.Products.Count);
  }

  public async Task<CatalogCategoryDTO?> Handle(GetCategoryByNameQuery queryParam, CancellationToken cancellationTokenParam)
  {
    var categories = (await _queryRepo.FindBySpecificationAsync(new CategoryByNameSpec(queryParam.Name))).ToList();
    if (categories.Count > 1)
    {
      throw new MoreThan1CategoryByNameException(queryParam.Name, categories.Count);
    }

    var categoryEntity = categories.SingleOrDefault();
    return categoryEntity == null
      ? null
      : new CatalogCategoryDTO(categoryEntity.CategoryId, categoryEntity.CategoryName, categoryEntity.Description, categoryEntity.Products.Count);
  }
}