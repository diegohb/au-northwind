namespace Infra.Persistence.EF.Entities.QueryViews;

public class SalesByCategoryView
{
  public int CategoryId { get; set; }
  public string CategoryName { get; set; }
  public string ProductName { get; set; }
  public decimal? ProductSales { get; set; }
}