namespace Infra.Persistence.EF.Entities;

public class ProductsByCategoryView
{
  public string CategoryName { get; set; }
  public bool Discontinued { get; set; }
  public string ProductName { get; set; }
  public string QuantityPerUnit { get; set; }
  public short? UnitsInStock { get; set; }
}