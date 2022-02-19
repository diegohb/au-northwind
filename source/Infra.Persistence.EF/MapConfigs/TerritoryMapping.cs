namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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