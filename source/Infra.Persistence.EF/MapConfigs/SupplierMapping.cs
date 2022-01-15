namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SupplierMapping : IEntityTypeConfiguration<Supplier>
{
  void IEntityTypeConfiguration<Supplier>.Configure(EntityTypeBuilder<Supplier> builderParam)
  {
    builderParam.HasIndex(e => e.CompanyName, "CompanyName");

    builderParam.HasIndex(e => e.PostalCode, "PostalCode");

    builderParam.Property(e => e.SupplierId).HasColumnName("SupplierID");

    builderParam.Property(e => e.Address).HasMaxLength(60);

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ContactName).HasMaxLength(30);

    builderParam.Property(e => e.ContactTitle).HasMaxLength(30);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.Fax).HasMaxLength(24);

    builderParam.Property(e => e.HomePage).HasColumnType("ntext");

    builderParam.Property(e => e.Phone).HasMaxLength(24);

    builderParam.Property(e => e.PostalCode).HasMaxLength(10);

    builderParam.Property(e => e.Region).HasMaxLength(15);
  }
}