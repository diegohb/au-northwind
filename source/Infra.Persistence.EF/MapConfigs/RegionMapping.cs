namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RegionMapping : IEntityTypeConfiguration<Region>
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