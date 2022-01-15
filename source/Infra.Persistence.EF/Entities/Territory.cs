namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Territory
{
  public Territory()
  {
    Employees = new HashSet<Employee>();
  }

  public virtual ICollection<Employee> Employees { get; set; }

  public virtual Region Region { get; set; }
  public int RegionId { get; set; }
  public string TerritoryDescription { get; set; }

  public string TerritoryId { get; set; }
}

public class TerritoryMapping : IEntityTypeConfiguration<Territory>
{
  public void Configure(EntityTypeBuilder<Territory> builderParam)
  {
    throw new NotImplementedException();
  }
}