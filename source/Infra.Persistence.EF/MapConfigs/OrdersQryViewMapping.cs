namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrdersQryViewMapping : IEntityTypeConfiguration<OrdersQryView>
{
  public void Configure(EntityTypeBuilder<OrdersQryView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Orders Qry");

    builderParam.Property(e => e.Address).HasMaxLength(60);

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.CustomerId)
      .HasMaxLength(5)
      .HasColumnName("CustomerID")
      .IsFixedLength();

    builderParam.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

    builderParam.Property(e => e.Freight).HasColumnType("money");

    builderParam.Property(e => e.OrderDate).HasColumnType("datetime");

    builderParam.Property(e => e.OrderId).HasColumnName("OrderID");

    builderParam.Property(e => e.PostalCode).HasMaxLength(10);

    builderParam.Property(e => e.Region).HasMaxLength(15);

    builderParam.Property(e => e.RequiredDate).HasColumnType("datetime");

    builderParam.Property(e => e.ShipAddress).HasMaxLength(60);

    builderParam.Property(e => e.ShipCity).HasMaxLength(15);

    builderParam.Property(e => e.ShipCountry).HasMaxLength(15);

    builderParam.Property(e => e.ShipName).HasMaxLength(40);

    builderParam.Property(e => e.ShipPostalCode).HasMaxLength(10);

    builderParam.Property(e => e.ShipRegion).HasMaxLength(15);

    builderParam.Property(e => e.ShippedDate).HasColumnType("datetime");
  }
}