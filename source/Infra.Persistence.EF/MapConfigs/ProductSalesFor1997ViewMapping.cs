namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductSalesFor1997ViewMapping : IEntityTypeConfiguration<ProductSalesFor1997View>
{
  public void Configure(EntityTypeBuilder<ProductSalesFor1997View> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Product Sales for 1997");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ProductSales).HasColumnType("money");
  }
}