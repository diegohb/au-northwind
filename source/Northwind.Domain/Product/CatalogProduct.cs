namespace Northwind.Domain.Product;

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

  public Guid Sku { get; private set; }

  #region Intentions

  public void ChangeSku(Guid skuParam)
  {
    if (Sku.Equals(skuParam))
    {
      throw new InvalidOperationException("Sku is not different.");
    }

    raiseEvent(new ProductSkuChangedEvent(Id, Sku, skuParam));
  }

  #endregion

  #region Mutations

  //((dynamic)this).Apply((dynamic) @event);

  protected void when(CatalogProductCreatedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Id = eventParam.AggregateId;
    Sku = eventParam.Sku;
  }

  protected void when(ProductSkuChangedEvent eventParam)
  {
    if (eventParam == null)
    {
      throw new ArgumentNullException(nameof(eventParam));
    }

    Sku = eventParam.NewSku;
  }

  #endregion
}

public class ProductId : IIdentityValueObject
{
  private const string idAsStringPrefix = "Product-";

  private ProductId(int idParam)
  {
    Id = idParam;
  }

  public ProductId(string idParam)
  {
    if (string.IsNullOrEmpty(idParam))
    {
      throw new ArgumentNullException(nameof(idParam));
    }

    Id = int.Parse(idParam.StartsWith(idAsStringPrefix) ? idParam.Substring(idAsStringPrefix.Length) : idParam);
  }

  public int Id { get; }

  public string IdAsString()
  {
    return $"{idAsStringPrefix}{Id.ToString()}";
  }

  public override bool Equals(object? objParam)
  {
    return objParam is ProductId id && Equals(Id, id.Id);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public static ProductId NewProductId(int productIdParam)
  {
    return new ProductId(productIdParam);
  }

  public static bool operator ==(ProductId left, ProductId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(ProductId left, ProductId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}

public class CategoryId : IIdentityValueObject
{
  private const string idAsStringPrefix = "Category-";

  private CategoryId(int idParam)
  {
    Id = idParam;
  }

  public CategoryId(string idParam)
  {
    if (string.IsNullOrEmpty(idParam))
    {
      throw new ArgumentNullException(nameof(idParam));
    }

    Id = int.Parse(idParam.StartsWith(idAsStringPrefix) ? idParam.Substring(idAsStringPrefix.Length) : idParam);
  }

  public int Id { get; }

  public string IdAsString()
  {
    return $"{idAsStringPrefix}{Id.ToString()}";
  }

  public override bool Equals(object? objParam)
  {
    return objParam is CategoryId id && Equals(Id, id.Id);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public static CategoryId NewCategoryId(int categoryIdParam)
  {
    return new CategoryId(categoryIdParam);
  }

  public static bool operator ==(CategoryId left, CategoryId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(CategoryId left, CategoryId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}

public class CatalogProductCreatedEvent : DomainEventBase<ProductId>
{
  internal CatalogProductCreatedEvent(ProductId aggregateIdParam, Guid skuParam) : base(aggregateIdParam)
  {
    Sku = skuParam;
  }

  private CatalogProductCreatedEvent(ProductId aggregateIdParam, long aggregateVersionParam, Guid skuParam) : base
    (aggregateIdParam, aggregateVersionParam)
  {
    Sku = skuParam;
  }

  public Guid Sku { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateIdParam, long aggregateVersionParam)
  {
    return new CatalogProductCreatedEvent(aggregateIdParam, aggregateVersionParam, Sku);
  }
}

public class ProductSkuChangedEvent : DomainEventBase<ProductId>
{
  internal ProductSkuChangedEvent(ProductId aggregateId, Guid oldSkuParam, Guid newSkuParam) : base(aggregateId)
  {
    OldSku = oldSkuParam;
    NewSku = newSkuParam;
  }

  private ProductSkuChangedEvent(ProductId aggregateId, long aggregateVersion, Guid oldSkuParam, Guid newSkuParam) : base
    (aggregateId, aggregateVersion)
  {
    OldSku = oldSkuParam;
    NewSku = newSkuParam;
  }

  public Guid NewSku { get; }
  public Guid OldSku { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductSkuChangedEvent(aggregateId, aggregateVersion, OldSku, NewSku);
  }
}