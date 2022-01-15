namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Region
{
  public Region()
  {
    Territories = new HashSet<Territory>();
  }

  public string RegionDescription { get; set; }

  public int RegionId { get; set; }

  public virtual ICollection<Territory> Territories { get; set; }
}

public class regionMapping : IEntityTypeConfiguration<Region>
{
  public void Configure(EntityTypeBuilder<Region> builderParam)
  {
    builderParam.HasKey(e => e.RegionId)
      .IsClustered(false);

    builderParam.ToTable("Region");

    builderParam.Property(e => e.RegionId)
      .ValueGeneratedNever()
      .HasColumnName("RegionID");

    builderParam.Property(e => e.RegionDescription)
      .IsRequired()
      .HasMaxLength(50)
      .IsFixedLength();
  }
}