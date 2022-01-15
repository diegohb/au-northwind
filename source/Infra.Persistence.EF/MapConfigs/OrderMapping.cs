namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
  void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builderParam)
  {
    builderParam.HasIndex(e => e.CustomerId, "CustomerID");

    builderParam.HasIndex(e => e.CustomerId, "CustomersOrders");

    builderParam.HasIndex(e => e.EmployeeId, "EmployeeID");

    builderParam.HasIndex(e => e.EmployeeId, "EmployeesOrders");

    builderParam.HasIndex(e => e.OrderDate, "OrderDate");

    builderParam.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

    builderParam.HasIndex(e => e.ShippedDate, "ShippedDate");

    builderParam.HasIndex(e => e.ShipVia, "ShippersOrders");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.CustomerId)
      .HasMaxLength(5)
      .HasColumnName("CustomerID")
      .IsFixedLength();

    builderParam.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

    builderParam.Property(e => e.Freight)
      .HasColumnType("money")
      .HasDefaultValueSql("((0))");

    builderParam.Property(e => e.OrderDate).HasColumnType("datetime");

    builderParam.Property(e => e.RequiredDate).HasColumnType("datetime");

    builderParam.Property(e => e.ShipAddress).HasMaxLength(60);

    builderParam.Property(e => e.ShipCity).HasMaxLength(15);

    builderParam.Property(e => e.ShipCountry).HasMaxLength(15);

    builderParam.Property(e => e.ShipName).HasMaxLength(40);

    builderParam.Property(e => e.ShipPostalCode).HasMaxLength(10);

    builderParam.Property(e => e.ShipRegion).HasMaxLength(15);

    builderParam.Property(e => e.ShippedDate).HasColumnType("datetime");

    builderParam.HasOne(d => d.Customer)
      .WithMany(p => p.Orders)
      .HasForeignKey(d => d.CustomerId)
      .HasConstraintName("FK_Orders_Customers");

    builderParam.HasOne(d => d.Employee)
      .WithMany(p => p.Orders)
      .HasForeignKey(d => d.EmployeeId)
      .HasConstraintName("FK_Orders_Employees");

    builderParam.HasOne(d => d.Shipper)
      .WithMany(p => p.Orders)
      .HasForeignKey(d => d.ShipVia)
      .HasConstraintName("FK_Orders_Shippers");
  }
}