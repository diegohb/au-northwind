namespace Infra.Persistence.EF.Entities
{
  using System;
  using System.Collections.Generic;

  public class Order
  {
    // related entities
    public Customer Customer { get; set; }
    public string CustomerID { get; set; }
    public Employee Employee { get; set; }
    public int EmployeeID { get; set; }
    public decimal? Freight { get; set; } = 0;
    public DateTime? OrderDate { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public int OrderID { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public Shipper Shipper { get; set; }
    public int ShipVia { get; set; }
  }
}