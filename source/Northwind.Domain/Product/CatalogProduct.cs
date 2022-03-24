namespace Northwind.Domain.Product;

using System.Diagnostics.CodeAnalysis;
using Core.Domain;

public class CatalogProduct : AggregateBase<ProductId>, IHaveIdentity<ProductId>
{
  public CatalogProduct() { }

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

  public string? Description { get; private set; }

  public string? Name { get; private set; }

  public Guid Sku { get; private set; }

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

  #endregion
}