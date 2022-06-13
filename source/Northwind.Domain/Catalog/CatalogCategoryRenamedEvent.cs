namespace Northwind.Domain.Catalog;

using Core.Domain;

public class CatalogCategoryRenamedEvent : DomainEventBase<CategoryId>
{
  internal CatalogCategoryRenamedEvent(CategoryId aggregateIdParam, string oldNameParam, string newNameParam) : base(aggregateIdParam)
  {
    OldName = oldNameParam;
    NewName = newNameParam;
  }

  private CatalogCategoryRenamedEvent(CategoryId aggregateIdParam, long aggregateVersionParam, string oldNameParam, string newNameParam) : base
    (aggregateIdParam, aggregateVersionParam)
  {
    OldName = oldNameParam;
    NewName = newNameParam;
  }

  public string NewName { get; }

  public string OldName { get; }

  public override IDomainEvent<CategoryId> WithAggregate(CategoryId aggregateIdParam, long aggregateVersionParam)
  {
    return new CatalogCategoryRenamedEvent(aggregateIdParam, aggregateVersionParam, OldName, NewName);
  }
}