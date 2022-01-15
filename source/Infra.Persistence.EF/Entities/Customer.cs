namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Customer
{
  public Customer()
  {
    Orders = new HashSet<Order>();
    CustomerTypes = new HashSet<CustomerDemographic>();
  }

  public string Address { get; set; }
  public string City { get; set; }
  public string CompanyName { get; set; }
  public string ContactName { get; set; }
  public string ContactTitle { get; set; }
  public string Country { get; set; }

  public string CustomerId { get; set; }

  public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; }
  public string Fax { get; set; }

  public virtual ICollection<Order> Orders { get; set; }
  public string Phone { get; set; }
  public string PostalCode { get; set; }
  public string Region { get; set; }
}

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
  void IEntityTypeConfiguration<Customer>.Configure(EntityTypeBuilder<Customer> builderParam)
  {
    builderParam.HasIndex(e => e.City, "City");

    builderParam.HasIndex(e => e.CompanyName, "CompanyName");

    builderParam.HasIndex(e => e.PostalCode, "PostalCode");

    builderParam.HasIndex(e => e.Region, "Region");

    builderParam.Property(e => e.CustomerId)
      .HasMaxLength(5)
      .HasColumnName("CustomerID")
      .IsFixedLength();

    builderParam.Property(e => e.Address).HasMaxLength(60);

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ContactName).HasMaxLength(30);

    builderParam.Property(e => e.ContactTitle).HasMaxLength(30);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.Fax).HasMaxLength(24);

    builderParam.Property(e => e.Phone).HasMaxLength(24);

    builderParam.Property(e => e.PostalCode).HasMaxLength(10);

    builderParam.Property(e => e.Region).HasMaxLength(15);

    builderParam.HasMany(e => e.Orders)
      .WithOne(e => e.Customer)
      .HasForeignKey(e => e.CustomerId)
      .HasConstraintName("FK_Orders_Customers");
  }
}