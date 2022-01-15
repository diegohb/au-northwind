namespace Infra.Persistence.EF.MapConfigs.QueryViews;

using Entities.QueryViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AlphabeticalListOfProductViewMapping : IEntityTypeConfiguration<AlphabeticalListOfProductView>
{
  public void Configure(EntityTypeBuilder<AlphabeticalListOfProductView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Alphabetical list of products");

    builderParam.Property(e => e.CategoryId).HasColumnName("CategoryID");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.ProductId).HasColumnName("ProductID");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.QuantityPerUnit).HasMaxLength(20);

    builderParam.Property(e => e.SupplierId).HasColumnName("SupplierID");

    builderParam.Property(e => e.UnitPrice).HasColumnType("money");
  }
}