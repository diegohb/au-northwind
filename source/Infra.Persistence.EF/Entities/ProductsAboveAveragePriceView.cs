namespace Infra.Persistence.EF.Entities;

public class ProductsAboveAveragePriceView
{
  public string ProductName { get; set; }
  public decimal? UnitPrice { get; set; }
}