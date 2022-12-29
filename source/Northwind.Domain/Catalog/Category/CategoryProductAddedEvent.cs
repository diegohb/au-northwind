namespace Northwind.Domain.Catalog.Category;

using Core.Domain;
using Product;

public class CategoryProductAddedEvent : DomainEventBase<CategoryId>
{
  internal CategoryProductAddedEvent(CategoryId aggregateIdParam, ProductId newProductIDParam) : base(aggregateIdParam)
  {
    NewProductID = newProductIDParam;
  }

  private CategoryProductAddedEvent(CategoryId aggregateIdParam, long aggregateVersionParam, ProductId newProductIDParam) : base
    (aggregateIdParam, aggregateVersionParam)
  {
    NewProductID = newProductIDParam;
  }

  public ProductId NewProductID { get; }

  public override IDomainEvent<CategoryId> WithAggregate(CategoryId aggregateIdParam, long aggregateVersionParam)
  {
    return new CategoryProductAddedEvent(aggregateIdParam, aggregateVersionParam, NewProductID);
  }
}