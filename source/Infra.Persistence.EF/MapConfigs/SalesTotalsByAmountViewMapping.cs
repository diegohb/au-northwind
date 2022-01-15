namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SalesTotalsByAmountViewMapping : IEntityTypeConfiguration<SalesTotalsByAmountView>
{
  public void Configure(EntityTypeBuilder<SalesTotalsByAmountView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Sales Totals by Amount");

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.SaleAmount).HasColumnType("money");

    builderParam.Property(e => e.ShippedDate).HasColumnType("datetime");
  }
}