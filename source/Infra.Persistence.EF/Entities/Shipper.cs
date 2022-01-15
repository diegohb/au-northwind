namespace Infra.Persistence.EF.Entities;

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

public class ShipperMapping : IEntityTypeConfiguration<Shipper>
{
  public void Configure(EntityTypeBuilder<Shipper> builderParam)
  {
    builderParam.Property(e => e.ShipperId).HasColumnName("ShipperID");

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.Phone).HasMaxLength(24);
  }
}