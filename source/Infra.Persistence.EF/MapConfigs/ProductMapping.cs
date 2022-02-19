namespace Infra.Persistence.EF.MapConfigs;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builderParam)
  {
    builderParam.HasIndex(e => e.CategoryId, "CategoriesProducts");

    builderParam.HasIndex(e => e.CategoryId, "CategoryID");

    builderParam.HasIndex(e => e.ProductName, "ProductName");

    builderParam.HasIndex(e => e.Sku, "Sku").HasDatabaseName("IX_Products_Sku");

    builderParam.HasIndex(e => e.SupplierId, "SupplierID");

    builderParam.HasIndex(e => e.SupplierId, "SuppliersProducts");

    builderParam.Property(e => e.ProductId).HasColumnName("ProductID");

    builderParam.Property(e => e.Sku).IsRequired().HasColumnName("Sku");

    builderParam.Property(e => e.CategoryId).HasColumnName("CategoryID");

    builderParam.Property(e => e.ProductName)
      .IsRequired()
      .HasMaxLength(40);

    builderParam.Property(e => e.Description)
      .IsRequired(false)
      .HasMaxLength(300);

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