namespace Infra.Persistence.EF.Entities
{
  public class Product
  {
    // related entities
    public Category Category { get; set; }
    public int? CategoryID { get; set; }
    public bool Discontinued { get; set; } = false;
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string QuantityPerUnit { get; set; }
    public short? ReorderLevel { get; set; } = 0;
    public Supplier Supplier { get; set; }
    public int? SupplierID { get; set; }
    public decimal? UnitPrice { get; set; } = 0;
    public short? UnitsInStock { get; set; } = 0;
    public short? UnitsOnOrder { get; set; } = 0;
  }
}