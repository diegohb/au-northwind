namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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