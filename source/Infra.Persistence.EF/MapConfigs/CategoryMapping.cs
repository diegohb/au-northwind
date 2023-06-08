namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builderParam)
  {
    builderParam.HasIndex(e => e.CategoryName, "CategoryName");

    builderParam.Property(e => e.CategoryId).HasColumnName("CategoryID");

    builderParam.Property(e => e.CategoryName)
      .IsRequired()
      .HasMaxLength(15);

    builderParam.Property(e => e.Description).HasColumnType("ntext");

    builderParam.Property(e => e.Picture).HasColumnType("image");

    builderParam.HasMany(e => e.Products)
      .WithOne(e => e.Category)
      .HasForeignKey(e => e.CategoryId)
      .HasConstraintName("FK_Products_Categories");

    builderParam.Navigation(e => e.Products).AutoInclude();
  }
}