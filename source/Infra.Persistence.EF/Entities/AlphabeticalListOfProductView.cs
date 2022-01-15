namespace Presentation.WebSPA.Scaffolded.Entities;

public class AlphabeticalListOfProductView
{
  public int? CategoryId { get; set; }
  public string CategoryName { get; set; }
  public bool Discontinued { get; set; }
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public string QuantityPerUnit { get; set; }
  public short? ReorderLevel { get; set; }
  public int? SupplierId { get; set; }
  public decimal? UnitPrice { get; set; }
  public short? UnitsInStock { get; set; }
  public short? UnitsOnOrder { get; set; }
}