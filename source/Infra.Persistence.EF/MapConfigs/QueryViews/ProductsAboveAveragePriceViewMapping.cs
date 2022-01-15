namespace Infra.Persistence.EF.MapConfigs.QueryViews;

using Entities.QueryViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductsAboveAveragePriceViewMapping : IEntityTypeConfiguration<ProductsAboveAveragePriceView>
{
  public void Configure(EntityTypeBuilder<ProductsAboveAveragePriceView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Products Above Average Price");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.UnitPrice).HasColumnType("money");
  }
}