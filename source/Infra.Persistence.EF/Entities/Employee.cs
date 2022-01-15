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
    throw new NotImplementedException();
  }
}