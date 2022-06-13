namespace Northwind.Domain.Catalog;

using Core.Domain;

public class CatalogCategoryCreatedEvent : DomainEventBase<CategoryId>
{
  internal CatalogCategoryCreatedEvent(CategoryId aggregateIdParam, string nameParam) : base(aggregateIdParam)
  {
    Name = nameParam;
  }

  private CatalogCategoryCreatedEvent(CategoryId aggregateIdParam, long aggregateVersionParam, string nameParam) : base
    (aggregateIdParam, aggregateVersionParam)
  {
    Name = nameParam;
  }

  public string Name { get; }

  public override IDomainEvent<CategoryId> WithAggregate(CategoryId aggregateIdParam, long aggregateVersionParam)
  {
    return new CatalogCategoryCreatedEvent(aggregateIdParam, aggregateVersionParam, Name);
  }
}