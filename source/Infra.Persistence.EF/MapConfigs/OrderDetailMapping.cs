namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderDetailMapping : IEntityTypeConfiguration<OrderDetail>
{
  public void Configure(EntityTypeBuilder<OrderDetail> builderParam)
  {
    builderParam.HasKey(e => new { e.OrderId, e.ProductId })
      .HasName("PK_Order_Details");

    builderParam.ToTable("Order Details");

    builderParam.HasIndex(e => e.OrderId, "OrderID");

    builderParam.HasIndex(e => e.OrderId, "OrdersOrder_Details");

    builderParam.HasIndex(e => e.ProductId, "ProductID");

    builderParam.HasIndex(e => e.ProductId, "ProductsOrder_Details");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.ProductId).HasColumnName("ProductID");

    builderParam.Property(e => e.Quantity).HasDefaultValueSql("((1))");

    builderParam.Property(e => e.UnitPrice).HasColumnType("money");

    builderParam.HasOne(d => d.Order)
      .WithMany(p => p.OrderDetails)
      .HasForeignKey(d => d.OrderId)
      .OnDelete(DeleteBehavior.ClientSetNull)
      .HasConstraintName("FK_Order_Details_Orders");

    builderParam.HasOne(d => d.Product)
      .WithMany(p => p.OrderDetails)
      .HasForeignKey(d => d.ProductId)
      .OnDelete(DeleteBehavior.ClientSetNull)
      .HasConstraintName("FK_Order_Details_Products");
  }
}