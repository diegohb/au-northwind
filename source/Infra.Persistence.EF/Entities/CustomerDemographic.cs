namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerDemographic
{
  public CustomerDemographic()
  {
    Customers = new HashSet<Customer>();
  }

  public string CustomerDesc { get; set; }

  public virtual ICollection<Customer> Customers { get; set; }

  public string CustomerTypeId { get; set; }
}

public class CustomerDemographicMapping : IEntityTypeConfiguration<CustomerDemographic>
{
  public void Configure(EntityTypeBuilder<CustomerDemographic> builderParam)
  {
    throw new NotImplementedException();
  }
}