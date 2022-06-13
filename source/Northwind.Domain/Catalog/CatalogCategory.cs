namespace Northwind.Domain.Catalog;

using System.Diagnostics.CodeAnalysis;
using Core.Domain;

public class CatalogCategory : AggregateBase<CategoryId>, IHaveIdentity<CategoryId>
{
  private CatalogCategory() { }

  public CatalogCategory(CategoryId categoryIdParam, string nameParam) : base(categoryIdParam)
  {
    if (categoryIdParam is null)
    {
      throw new ArgumentNullException(nameof(categoryIdParam));
    }

    if (string.IsNullOrWhiteSpace(nameParam))
    {
      throw new ArgumentNullException(nameof(nameParam));
    }

    raiseEvent(new CatalogCategoryCreatedEvent(Id, nameParam));
  }

  public string? DisplayName { get; private set; }

  #region Mutations

  //((dynamic)this).Apply((dynamic) @event);

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(CatalogCategoryCreatedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Id = eventParam.AggregateId;
    DisplayName = eventParam.Name;
  }

  #endregion
}