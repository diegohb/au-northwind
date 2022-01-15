namespace Infra.Persistence.EF.Entities;

public class OrderDetailsExtendedView
{
  public float Discount { get; set; }
  public decimal? ExtendedPrice { get; set; }
  public int OrderId { get; set; }
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public short Quantity { get; set; }
  public decimal UnitPrice { get; set; }
}