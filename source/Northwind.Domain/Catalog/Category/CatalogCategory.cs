namespace Northwind.Domain.Catalog.Category;

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Core.Domain;
using Product;

public class CatalogCategory : AggregateBase<CategoryId>, IHaveIdentity<CategoryId>
{
  private readonly HashSet<ProductId> _products = new();

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

  public ImmutableHashSet<ProductId> Products => _products.ToImmutableHashSet();

  public void AddProduct(ProductId newProductIdParam)
  {
    if (_products.Contains(newProductIdParam))
    {
      throw new InvalidOperationException("Product is already categorized.");
    }

    raiseEvent(new CategoryProductAddedEvent(Id, newProductIdParam));
  }

  public void AddProduct(params ProductId[] newProductIdParams)
  {
    if (newProductIdParams.Length == 0)
    {
      throw new NullReferenceException(nameof(newProductIdParams));
    }

    try
    {
      Array.ForEach(newProductIdParams, newProductID => AddProduct(newProductID));
    }
    catch (InvalidOperationException exAlreadyCategorized) when (exAlreadyCategorized.Message.Equals("Product is already categorized."))
    {
      throw new InvalidOperationException("At least one of the products is already categorized.", exAlreadyCategorized);
    }
  }

  public void ChangeName(string newNameParam)
  {
    if (string.IsNullOrWhiteSpace(newNameParam))
    {
      throw new NullReferenceException(nameof(newNameParam));
    }

    if (string.IsNullOrWhiteSpace(DisplayName))
    {
      throw new InvalidOperationException("The name has not been set. This is an unexpected error.");
    }

    if (DisplayName.Equals(newNameParam))
    {
      throw new InvalidOperationException("The new name is the same.");
    }

    raiseEvent(new CatalogCategoryRenamedEvent(Id, DisplayName, newNameParam));
  }

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

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(CatalogCategoryRenamedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    DisplayName = eventParam.NewName;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(CategoryProductAddedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    _products.Add(eventParam.NewProductID);
  }

  #endregion
}