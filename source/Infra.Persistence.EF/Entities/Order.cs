namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;

public class Order
{
  public Order()
  {
    OrderDetails = new HashSet<OrderDetail>();
  }

  public virtual Customer Customer { get; set; }
  public string CustomerId { get; set; }
  public virtual Employee Employee { get; set; }
  public int? EmployeeId { get; set; }
  public decimal? Freight { get; set; }
  public DateTime? OrderDate { get; set; }
  public virtual ICollection<OrderDetail> OrderDetails { get; set; }

  public int OrderId { get; set; }
  public DateTime? RequiredDate { get; set; }
  public string ShipAddress { get; set; }
  public string ShipCity { get; set; }
  public string ShipCountry { get; set; }
  public string ShipName { get; set; }
  public DateTime? ShippedDate { get; set; }
  public virtual Shipper Shipper { get; set; }
  public string ShipPostalCode { get; set; }
  public string ShipRegion { get; set; }
  public int? ShipVia { get; set; }
}