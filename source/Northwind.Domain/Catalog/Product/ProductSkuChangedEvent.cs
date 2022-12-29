namespace Northwind.Domain.Catalog.Product;

using Core.Domain;

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