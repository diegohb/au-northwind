namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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