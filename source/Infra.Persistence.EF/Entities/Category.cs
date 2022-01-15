namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Category
{
  public Category()
  {
    Products = new HashSet<Product>();
  }

  public int CategoryId { get; set; }
  public string CategoryName { get; set; }
  public string Description { get; set; }
  public byte[] Picture { get; set; }

  public virtual ICollection<Product> Products { get; set; }
}

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
  }
}