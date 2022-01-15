namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerAndSuppliersByCityViewMapping : IEntityTypeConfiguration<CustomerAndSuppliersByCityView>
{
  public void Configure(EntityTypeBuilder<CustomerAndSuppliersByCityView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Customer and Suppliers by City");

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ContactName).HasMaxLength(30);

    builderParam.Property(e => e.Relationship)
      .IsRequired()
      .HasMaxLength(9)
      .IsUnicode(false);
  }
}