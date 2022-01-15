namespace Infra.Persistence.EF.MapConfigs.QueryViews;

using Entities.QueryViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuarterlyOrderViewMapping : IEntityTypeConfiguration<QuarterlyOrderView>
{
  public void Configure(EntityTypeBuilder<QuarterlyOrderView> builderParam)
  {
    builderParam.HasNoKey();

    builderParam.ToView("Quarterly Orders");

    builderParam.Property(e => e.City).HasMaxLength(15);

    builderParam.Property(e => e.CompanyName).HasMaxLength(40);

    builderParam.Property(e => e.Country).HasMaxLength(15);

    builderParam.Property(e => e.CustomerId)
      .HasMaxLength(5)
      .HasColumnName("CustomerID")
      .IsFixedLength();
  }
}