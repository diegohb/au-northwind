namespace Infra.Persistence.EF.MapConfigs;

using Entities;
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

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);
  }
}