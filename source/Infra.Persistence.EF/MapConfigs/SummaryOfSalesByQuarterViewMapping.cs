namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SummaryOfSalesByQuarterViewMapping : IEntityTypeConfiguration<SummaryOfSalesByQuarterView>
{
  public void Configure(EntityTypeBuilder<SummaryOfSalesByQuarterView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Summary of Sales by Quarter");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.ShippedDate).HasColumnType("datetime");

    builderParam.Property(e => e.Subtotal).HasColumnType("money");
  }
}