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
    // entity =>
    // {
    //   entity.HasIndex(e => e.CategoryName, "CategoryName");
    //
    //   entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
    //
    //   entity.Property(e => e.CategoryName)
    //     .IsRequired()
    //     .HasMaxLength(15);
    //
    //   entity.Property(e => e.Description).HasColumnType("ntext");
    //
    //   entity.Property(e => e.Picture).HasColumnType("image");
    //
    //   entity.HasMany(e => e.Products)
    //     .WithOne(e => e.Category)
    //     .HasForeignKey(e => e.CategoryId)
    //     .HasConstraintName("FK_Products_Categories");
    // }
  }
}