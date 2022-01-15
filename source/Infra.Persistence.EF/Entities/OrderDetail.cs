namespace Infra.Persistence.EF.Entities
{
  public class OrderDetail
  {
    public double Discount { get; set; } = 0;

    // related entities
    public Order Order { get; set; }

    public int OrderID { get; set; }
    public Product Product { get; set; }
    public int ProductID { get; set; }
    public short Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; } = 0;
  }
}