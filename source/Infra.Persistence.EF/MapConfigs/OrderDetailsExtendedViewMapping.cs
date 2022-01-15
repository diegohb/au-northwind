namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderDetailsExtendedViewMapping : IEntityTypeConfiguration<OrderDetailsExtendedView>
{
  public void Configure(EntityTypeBuilder<OrderDetailsExtendedView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Order Details Extended");

    builderParam.Property(e => e.ExtendedPrice).HasColumnType("money");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.ProductId).HasColumnName("ProductID");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.UnitPrice).HasColumnType("money");
  }
}