namespace Infra.Persistence.EF.Entities;

public class OrderSubtotalView
{
  public int OrderId { get; set; }
  public decimal? Subtotal { get; set; }
}