namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public class OrderMapping : IEntityTypeConfiguration<Order>
{
  void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builderParam)
  {
    throw new NotImplementedException();
  }
}