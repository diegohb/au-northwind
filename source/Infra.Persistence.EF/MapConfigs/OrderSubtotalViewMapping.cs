namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderSubtotalViewMapping : IEntityTypeConfiguration<OrderSubtotalView>
{
  public void Configure(EntityTypeBuilder<OrderSubtotalView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Order Subtotals");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.Subtotal).HasColumnType("money");
  }
}