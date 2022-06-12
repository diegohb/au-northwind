namespace Northwind.Domain.Product;

using Core.Domain;

public class ProductDescribedEvent : DomainEventBase<ProductId>
{
  internal ProductDescribedEvent(ProductId aggregateIdParam, string? oldDescriptionParam, string newDescriptionParam)
    : base(aggregateIdParam)
  {
    OldDescription = oldDescriptionParam;
    NewDescription = newDescriptionParam;
  }

  private ProductDescribedEvent(ProductId aggregateIdParam, long aggregateVersionParam, string? oldDescriptionParam, string newDescriptionParam)
    : base(aggregateIdParam, aggregateVersionParam)
  {
    OldDescription = oldDescriptionParam;
    NewDescription = newDescriptionParam;
  }

  public string NewDescription { get; }

  public string? OldDescription { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductDescribedEvent(aggregateId, aggregateVersion, OldDescription, NewDescription);
  }
}