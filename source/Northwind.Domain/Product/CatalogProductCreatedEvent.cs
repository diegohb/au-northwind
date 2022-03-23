namespace Northwind.Domain.Product;

using Core.Domain;

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