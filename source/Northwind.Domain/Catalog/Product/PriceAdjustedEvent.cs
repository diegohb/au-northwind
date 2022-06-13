namespace Northwind.Domain.Catalog.Product;

using Core.Domain;

public class PriceAdjustedEvent : DomainEventBase<ProductId>
{
  private PriceAdjustedEvent(ProductId aggregateIdParam, long aggregateVersionParam, decimal amountParam, string changeCommentParam) : base
    (aggregateIdParam, aggregateVersionParam)
  {
    initializeValues(amountParam);
    ChangeComment = changeCommentParam;
  }

  internal PriceAdjustedEvent(ProductId aggregateIdParam, decimal amountParam, string changeCommentParam) : base(aggregateIdParam)
  {
    initializeValues(amountParam);
    ChangeComment = changeCommentParam;
  }

  public PriceAdjustmentTypeEnum AdjustmentType { get; private set; }

  public decimal Amount { get; private set; }

  public string ChangeComment { get; }

  public override IDomainEvent<ProductId> WithAggregate(ProductId aggregateId, long aggregateVersion)
  {
    return new PriceAdjustedEvent(aggregateId, aggregateVersion, Amount, ChangeComment);
  }

  #region Support Methods

  private void initializeValues(decimal amountParam)
  {
    AdjustmentType = amountParam < 0 ? PriceAdjustmentTypeEnum.Decrease : PriceAdjustmentTypeEnum.Increase;
    Amount = amountParam;
  }

  #endregion
}