namespace Northwind.Domain.Product;

using System.Diagnostics.CodeAnalysis;
using Core.Domain;

public class CatalogProduct : AggregateBase<ProductId>, IHaveIdentity<ProductId>
{
  private CatalogProduct() { }

  public CatalogProduct(ProductId productIdParam, Guid skuParam) : base(productIdParam)
  {
    if (productIdParam is null)
    {
      throw new ArgumentNullException(nameof(productIdParam));
    }

    if (skuParam == Guid.Empty)
    {
      throw new ArgumentNullException(nameof(skuParam));
    }

    raiseEvent(new CatalogProductCreatedEvent(Id, skuParam));
  }

  public CategoryId? CategoryID { get; private set; }

  public string? Description { get; private set; }

  public bool ListedInCatalog { get; private set; }

  public DateTime? ListingExpiration { get; private set; }

  public string? Name { get; private set; }

  public decimal Price { get; private set; }

  public Guid Sku { get; private set; }

  public void Categorize(CategoryId categoryIdParam)
  {
    if (categoryIdParam == null)
    {
      throw new ArgumentNullException(nameof(categoryIdParam));
    }

    if (CategoryID == null)
    {
      raiseEvent(new ProductCategorizedEvent(Id, categoryIdParam));
    }
    else
    {
      if (CategoryID.Equals(categoryIdParam))
      {
        throw new InvalidOperationException("Category ID provided is not different.");
      }

      raiseEvent(new ProductRecategorizedEvent(Id, CategoryID, categoryIdParam));
    }
  }

  public void DescribeProduct(string newDescriptionParam)
  {
    if (string.IsNullOrEmpty(newDescriptionParam))
    {
      throw new ArgumentNullException(nameof(newDescriptionParam));
    }

    raiseEvent(new ProductDescribedEvent(Id, Description, newDescriptionParam));
  }

  #region Intentions

  public void ChangeSku(Guid skuParam)
  {
    if (Sku.Equals(skuParam))
    {
      throw new InvalidOperationException("Sku is not different.");
    }

    raiseEvent(new ProductSkuChangedEvent(Id, Sku, skuParam));
  }

  public void Rename(string? newProductNameParam)
  {
    if (string.IsNullOrEmpty(newProductNameParam))
    {
      throw new ArgumentNullException(nameof(newProductNameParam));
    }

    if (!string.IsNullOrEmpty(Name) && Name.Equals(newProductNameParam, StringComparison.CurrentCultureIgnoreCase))
    {
      throw new InvalidOperationException("Name is not different.");
    }

    raiseEvent(new ProductRenamedEvent(Id, Name, newProductNameParam));
  }

  public void List(DateTime? listingExpirationParam = null)
  {
    if (ListedInCatalog)
    {
      throw new InvalidOperationException("The product is already listed.");
    }

    if (ListingExpiration.HasValue && ListingExpiration.Equals(listingExpirationParam))
    {
      throw new InvalidOperationException("The desired expiration is the same.");
    }

    if (listingExpirationParam < DateTime.UtcNow)
    {
      throw new InvalidOperationException("The listing expiration cannot be in the past.");
    }

    raiseEvent(new ProductListedEvent(Id, listingExpirationParam));
  }

  public void Unlist()
  {
    if (!ListedInCatalog)
    {
      throw new InvalidOperationException("Product is already unlisted.");
    }

    raiseEvent(new ProductUnlistedEvent(Id));
  }

  public void IncreaseListPrice(decimal amountParam, string changeCommentParam)
  {
    if (amountParam < 0)
    {
      throw new InvalidOperationException("Change amount must not be negative.");
    }

    raiseEvent(new PriceAdjustedEvent(Id, amountParam, changeCommentParam));
  }

  public void DecreaseListPrice(decimal amountParam, string changeCommentParam)
  {
    if (amountParam < 0)
    {
      throw new InvalidOperationException("Change amount must not be negative.");
    }

    raiseEvent(new PriceAdjustedEvent(Id, 0 - amountParam, changeCommentParam));
  }

  #endregion

  #region Mutations

  //((dynamic)this).Apply((dynamic) @event);

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(CatalogProductCreatedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Id = eventParam.AggregateId;
    Sku = eventParam.Sku;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductSkuChangedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Sku = eventParam.NewSku;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductRenamedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Name = eventParam.NewName;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductDescribedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Description = eventParam.NewDescription;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductListedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    if (eventParam.ListingExpiresAt.HasValue)
    {
      ListingExpiration = eventParam.ListingExpiresAt;
      ListedInCatalog = eventParam.ListingExpiresAt > DateTime.UtcNow; //NOTE: this accounts for setting correct false value when rehydrating
    }
    else
    {
      ListedInCatalog = true;
    }
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductUnlistedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    ListedInCatalog = false;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(PriceAdjustedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Price += eventParam.Amount;
  }


  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductCategorizedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    CategoryID = eventParam.CategoryID;
  }

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  protected void when(ProductRecategorizedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    CategoryID = eventParam.NewCategoryID;
  }

  #endregion
}