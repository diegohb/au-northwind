namespace Infra.Persistence.EF.Entities;

public class OrderDetail
{
  public float Discount { get; set; }

  public virtual Order Order { get; set; }
  public int OrderId { get; set; }
  public virtual Product Product { get; set; }
  public int ProductId { get; set; }
  public short Quantity { get; set; }
  public decimal UnitPrice { get; set; }
}