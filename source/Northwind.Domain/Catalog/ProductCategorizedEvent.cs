namespace Northwind.Domain.Catalog;

using Core.Domain;

public class ProductCategorizedEvent : DomainEventBase<ProductId>
{
  private ProductCategorizedEvent(ProductId aggregateIdParam, long aggregateVersionParam, CategoryId categoryIdParam)
    : base(aggregateIdParam, aggregateVersionParam)
  {
    CategoryID = categoryIdParam;
  }

  internal ProductCategorizedEvent(ProductId productIdParam, CategoryId categoryIdParam) : base(productIdParam)
  {
    CategoryID = categoryIdParam;
  }

  public CategoryId CategoryID { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductCategorizedEvent(aggregateId, aggregateVersion, CategoryID);
  }
}