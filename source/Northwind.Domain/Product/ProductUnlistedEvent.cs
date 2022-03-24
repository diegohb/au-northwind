namespace Northwind.Domain.Product;

using Core.Domain;

public class ProductUnlistedEvent : DomainEventBase<ProductId>
{
  internal ProductUnlistedEvent(ProductId aggregateId) : base(aggregateId) { }

  private ProductUnlistedEvent(ProductId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion) { }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductUnlistedEvent(aggregateId, aggregateVersion);
  }
}