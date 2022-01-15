namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductsByCategoryViewMapping : IEntityTypeConfiguration<ProductsByCategoryView>
{
  public void Configure(EntityTypeBuilder<ProductsByCategoryView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Products by Category");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.QuantityPerUnit).HasMaxLength(20);
  }
}