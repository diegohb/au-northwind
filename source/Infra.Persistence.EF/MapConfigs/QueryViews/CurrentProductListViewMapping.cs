namespace Infra.Persistence.EF.MapConfigs.QueryViews;

using Entities.QueryViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CurrentProductListViewMapping : IEntityTypeConfiguration<CurrentProductListView>
{
  public void Configure(EntityTypeBuilder<CurrentProductListView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Current Product List");

    builderParam.Property(e => e.ProductId)
      .ValueGeneratedOnAdd()
      .HasColumnName("ProductID");

    builderParam.Property(e => e.Sku).IsRequired().HasColumnName("Sku");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);
  }
}