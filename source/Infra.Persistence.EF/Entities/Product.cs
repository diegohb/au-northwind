namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class Product
{
  public Product()
  {
    OrderDetails = new HashSet<OrderDetail>();
  }

  public virtual Category Category { get; set; }
  public int? CategoryId { get; set; }
  public bool Discontinued { get; set; }
  public virtual ICollection<OrderDetail> OrderDetails { get; set; }

  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public string QuantityPerUnit { get; set; }
  public short? ReorderLevel { get; set; }
  public virtual Supplier Supplier { get; set; }
  public int? SupplierId { get; set; }
  public decimal? UnitPrice { get; set; }
  public short? UnitsInStock { get; set; }
  public short? UnitsOnOrder { get; set; }
}