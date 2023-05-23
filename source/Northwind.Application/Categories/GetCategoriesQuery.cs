namespace Northwind.Application.Categories;

using Abstractions;
using Infra.Persistence.EF.Entities;

public record GetCategoriesQuery : IQuery<IList<Category>>;

public record GetCategoryByIDQuery(int CategoryID) : IQuery<Category?>;

public record GetCategoryByNameQuery(string Name) : IQuery<Category?>;