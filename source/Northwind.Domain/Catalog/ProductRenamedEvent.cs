namespace Northwind.Domain.Catalog;

using Core.Domain;

public class ProductRenamedEvent : DomainEventBase<ProductId>
{
  internal ProductRenamedEvent(ProductId aggregateId, string? oldNameParam, string? newNameParam) : base(aggregateId)
  {
    OldName = oldNameParam;
    NewName = newNameParam;
  }

  private ProductRenamedEvent(ProductId aggregateId, long aggregateVersion, string? oldNameParam, string? newNameParam) : base
    (aggregateId, aggregateVersion)
  {
    OldName = oldNameParam;
    NewName = newNameParam;
  }

  public string? NewName { get; }
  public string? OldName { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductRenamedEvent(aggregateId, aggregateVersion, OldName, NewName);
  }
}