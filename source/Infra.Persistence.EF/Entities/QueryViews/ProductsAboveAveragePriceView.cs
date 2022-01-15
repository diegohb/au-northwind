namespace Infra.Persistence.EF.Entities.QueryViews;

public class ProductsAboveAveragePriceView
{
  public string ProductName { get; set; }
  public decimal? UnitPrice { get; set; }
}