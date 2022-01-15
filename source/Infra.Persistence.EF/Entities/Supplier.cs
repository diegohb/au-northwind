namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Supplier
{
  public Supplier()
  {
    Products = new HashSet<Product>();
  }

  public string Address { get; set; }
  public string City { get; set; }
  public string CompanyName { get; set; }
  public string ContactName { get; set; }
  public string ContactTitle { get; set; }
  public string Country { get; set; }
  public string Fax { get; set; }
  public string HomePage { get; set; }
  public string Phone { get; set; }
  public string PostalCode { get; set; }

  public virtual ICollection<Product> Products { get; set; }
  public string Region { get; set; }

  public int SupplierId { get; set; }
}

public class SupplierMapping : IEntityTypeConfiguration<Supplier>
{
  void IEntityTypeConfiguration<Supplier>.Configure(EntityTypeBuilder<Supplier> builderParam)
  {
    builderParam.HasIndex(e => e.CompanyName, "CompanyName");

    builderParam.HasIndex(e => e.PostalCode, "PostalCode");

    builderParam.Property(e => e.SupplierId).HasColumnName("SupplierID");

    builderParam.Property(e => e.Address).HasMaxLength(60);

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.ContactName).HasMaxLength(30);

    builderParam.Property(e => e.ContactTitle).HasMaxLength(30);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.Fax).HasMaxLength(24);

    builderParam.Property(e => e.HomePage).HasColumnType("ntext");

    builderParam.Property(e => e.Phone).HasMaxLength(24);

    builderParam.Property(e => e.PostalCode).HasMaxLength(10);

    builderParam.Property(e => e.Region).HasMaxLength(15);
  }
}