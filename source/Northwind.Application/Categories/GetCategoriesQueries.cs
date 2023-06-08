namespace Northwind.Application.Categories;

using Abstractions;
using ErrorOr;

public record GetCategoriesQuery : IQuery<ErrorOr<IList<CatalogCategoryDTO>>>;

public record GetCategoryByIDQuery(int CategoryID) : IQuery<ErrorOr<CatalogCategoryDTO>>;

public record GetCategoryByNameQuery(string Name) : IQuery<ErrorOr<CatalogCategoryDTO>>;