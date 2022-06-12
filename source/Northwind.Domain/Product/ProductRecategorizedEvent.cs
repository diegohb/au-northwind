namespace Northwind.Domain.Product;

using Core.Domain;

public class ProductRecategorizedEvent : DomainEventBase<ProductId>
{
  private ProductRecategorizedEvent
    (ProductId aggregateIdParam, long aggregateVersionParam, CategoryId oldCategoryIdParam, CategoryId newCategoryIdParam)
    : base(aggregateIdParam, aggregateVersionParam)
  {
    OldCategoryID = oldCategoryIdParam;
    NewCategoryID = newCategoryIdParam;
  }

  internal ProductRecategorizedEvent(ProductId productIdParam, CategoryId oldCategoryIdParam, CategoryId newCategoryIdParam) : base(productIdParam)
  {
    OldCategoryID = oldCategoryIdParam;
    NewCategoryID = newCategoryIdParam;
  }

  public CategoryId NewCategoryID { get; }

  public CategoryId OldCategoryID { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new ProductRecategorizedEvent(aggregateId, aggregateVersion, OldCategoryID, NewCategoryID);
  }
}