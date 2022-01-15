namespace Infra.Persistence.EF.MapConfigs.QueryViews;

using Entities.QueryViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesByCategoryViewMapping : IEntityTypeConfiguration<SalesByCategoryView>
{
  public void Configure(EntityTypeBuilder<SalesByCategoryView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Sales by Category");

    builderParam.Property(e => e.CategoryId).HasColumnName("CategoryID");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ProductSales).HasColumnType("money");
  }
}