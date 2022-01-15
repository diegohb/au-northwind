namespace Infra.Persistence.EF.Entities;

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
    builderParam.HasKey(e => e.CustomerTypeId)
      .IsClustered(false);

    builderParam.Property(e => e.CustomerTypeId)
      .HasMaxLength(10)
      .HasColumnName("CustomerTypeID")
      .IsFixedLength();

    builderParam.Property(e => e.CustomerDesc).HasColumnType("ntext");
  }
}