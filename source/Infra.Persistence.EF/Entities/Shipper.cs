namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Shipper
{
  public Shipper()
  {
    Orders = new HashSet<Order>();
  }

  public string CompanyName { get; set; }

  public virtual ICollection<Order> Orders { get; set; }
  public string Phone { get; set; }

  public int ShipperId { get; set; }
}

public class ShipperMapping : IEntityTypeConfiguration<ShipperMapping>
{
  void IEntityTypeConfiguration<ShipperMapping>.Configure(EntityTypeBuilder<ShipperMapping> builderParam)
  {
    throw new NotImplementedException();
  }
}