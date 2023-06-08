namespace Northwind.Application.Categories;

using Abstractions;

public record GetCategoriesQuery : IQuery<IList<CatalogCategoryDTO>>;

public record GetCategoryByIDQuery(int CategoryID) : IQuery<CatalogCategoryDTO?>;

public record GetCategoryByNameQuery(string Name) : IQuery<CatalogCategoryDTO?>;