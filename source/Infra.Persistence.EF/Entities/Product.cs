namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Product
{
  public Product()
  {
    OrderDetails = new HashSet<OrderDetail>();
  }

  public virtual Category Category { get; set; }
  public int? CategoryId { get; set; }
  public bool Discontinued { get; set; }
  public virtual ICollection<OrderDetail> OrderDetails { get; set; }

  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public string QuantityPerUnit { get; set; }
  public short? ReorderLevel { get; set; }
  public virtual Supplier Supplier { get; set; }
  public int? SupplierId { get; set; }
  public decimal? UnitPrice { get; set; }
  public short? UnitsInStock { get; set; }
  public short? UnitsOnOrder { get; set; }
}

public class ProductMapping : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builderParam)
  {
    builderParam.HasIndex(e => e.CategoryId, "CategoriesProducts");

    builderParam.HasIndex(e => e.CategoryId, "CategoryID");

    builderParam.HasIndex(e => e.ProductName, "ProductName");

    builderParam.HasIndex(e => e.SupplierId, "SupplierID");

    builderParam.HasIndex(e => e.SupplierId, "SuppliersProducts");

    builderParam.Property(e => e.ProductId).HasColumnName("ProductID");

    builderParam.Property(e => e.CategoryId).HasColumnName("CategoryID");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.QuantityPerUnit).HasMaxLength(20);

    builderParam.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

    builderParam.Property(e => e.SupplierId).HasColumnName("SupplierID");

    builderParam.Property(e => e.UnitPrice)
      .HasColumnType("money")
      .HasDefaultValueSql("((0))");

    builderParam.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

    builderParam.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

    builderParam.HasOne(d => d.Category)
      .WithMany(p => p.Products)
      .HasForeignKey(d => d.CategoryId)
      .HasConstraintName("FK_Products_Categories");

    builderParam.HasOne(d => d.Supplier)
      .WithMany(p => p.Products)
      .HasForeignKey(d => d.SupplierId)
      .HasConstraintName("FK_Products_Suppliers");

    //entity.Navigation(e => e.OrderDetails).AutoInclude(false); //NOTE: to not automatically eager load related data
  }
}