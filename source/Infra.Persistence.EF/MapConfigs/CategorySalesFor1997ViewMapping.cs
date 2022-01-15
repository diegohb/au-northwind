namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategorySalesFor1997ViewMapping : IEntityTypeConfiguration<CategorySalesFor1997View>
{
  public void Configure(EntityTypeBuilder<CategorySalesFor1997View> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Category Sales for 1997");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.CategorySales).HasColumnType("money");
  }
}