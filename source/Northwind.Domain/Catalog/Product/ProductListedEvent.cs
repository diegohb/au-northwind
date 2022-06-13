namespace Northwind.Domain.Catalog.Product;

using Core.Domain;

public class ProductListedEvent : DomainEventBase<ProductId>
{
  internal ProductListedEvent(ProductId aggregateId, DateTime? listingExpiresAtParam) : base(aggregateId)
  {
    ListingExpiresAt = listingExpiresAtParam;
  }

  private ProductListedEvent(ProductId aggregateId, long aggregateVersion, DateTime? listingExpiresAtParam) : base
    (aggregateId, aggregateVersion)
  {
    ListingExpiresAt = listingExpiresAtParam;
  }

  public DateTime? ListingExpiresAt { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductListedEvent(aggregateId, aggregateVersion, ListingExpiresAt);
  }
}