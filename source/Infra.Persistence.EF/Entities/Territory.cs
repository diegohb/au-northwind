namespace Infra.Persistence.EF.Entities;

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
    builderParam.HasKey(e => e.TerritoryId)
      .IsClustered(false);

    builderParam.Property(e => e.TerritoryId)
      .HasMaxLength(20)
      .HasColumnName("TerritoryID");

    builderParam.Property(e => e.RegionId).HasColumnName("RegionID");

    builderParam.Property(e => e.TerritoryDescription)
      .IsRequired()
      .HasMaxLength(50)
      .IsFixedLength();

    builderParam.HasOne(d => d.Region)
      .WithMany(p => p.Territories)
      .HasForeignKey(d => d.RegionId)
      .OnDelete(DeleteBehavior.ClientSetNull)
      .HasConstraintName("FK_Territories_Region");
  }
}