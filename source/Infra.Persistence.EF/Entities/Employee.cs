namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Employee
{
  public Employee()
  {
    Orders = new HashSet<Order>();
    Territories = new HashSet<Territory>();
  }

  public string Address { get; set; }
  public DateTime? BirthDate { get; set; }
  public string City { get; set; }
  public string Country { get; set; }

  public int EmployeeId { get; set; }
  public string Extension { get; set; }
  public string FirstName { get; set; }
  public DateTime? HireDate { get; set; }
  public string HomePhone { get; set; }
  public string LastName { get; set; }
  public string Notes { get; set; }

  public virtual ICollection<Order> Orders { get; set; }
  public byte[] Photo { get; set; }
  public string PhotoPath { get; set; }
  public string PostalCode { get; set; }
  public string Region { get; set; }
  public int? ReportsTo { get; set; }

  public virtual ICollection<Territory> Territories { get; set; }
  public string Title { get; set; }
  public string TitleOfCourtesy { get; set; }
}

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
  public void Configure(EntityTypeBuilder<Employee> builderParam)
  {
    builderParam.HasIndex(e => e.LastName, "LastName");

    builderParam.HasIndex(e => e.PostalCode, "PostalCode");

    builderParam.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

    builderParam.Property(e => e.Address).HasMaxLength(60);

    builderParam.Property(e => e.BirthDate).HasColumnType("datetime");

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.Extension).HasMaxLength(4);

    builderParam.Property(e => e.FirstName)
      .IsRequired()
      .HasMaxLength(10);

    builderParam.Property(e => e.HireDate).HasColumnType("datetime");

    builderParam.Property(e => e.HomePhone).HasMaxLength(24);

    builderParam.Property(e => e.LastName)
      .IsRequired()
      .HasMaxLength(20);

    builderParam.Property(e => e.Notes).HasColumnType("ntext");

    builderParam.Property(e => e.Photo).HasColumnType("image");

    builderParam.Property(e => e.PhotoPath).HasMaxLength(255);

    builderParam.Property(e => e.PostalCode).HasMaxLength(10);

    builderParam.Property(e => e.Region).HasMaxLength(15);

    builderParam.Property(e => e.Title).HasMaxLength(30);

    builderParam.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

    builderParam.HasMany(d => d.Territories)
      .WithMany(p => p.Employees)
      .UsingEntity<Dictionary<string, object>>
      (
        "EmployeeTerritory",
        l => l.HasOne<Territory>()
          .WithMany()
          .HasForeignKey("TerritoryId")
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName
            ("FK_EmployeeTerritories_Territories"),
        r => r.HasOne<Employee>()
          .WithMany()
          .HasForeignKey("EmployeeId")
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName
            ("FK_EmployeeTerritories_Employees"),
        j =>
        {
          j.HasKey("EmployeeId", "TerritoryId").IsClustered(false);

          j.ToTable("EmployeeTerritories");

          j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");

          j.IndexerProperty<string>("TerritoryId").HasMaxLength(20).HasColumnName("TerritoryID");
        });

    builderParam.HasMany(e => e.Orders)
      .WithOne(e => e.Employee)
      .HasForeignKey(e => e.EmployeeId)
      .HasConstraintName("FK_Orders_Employees");
  }
}