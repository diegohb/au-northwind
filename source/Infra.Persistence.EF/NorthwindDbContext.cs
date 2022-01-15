namespace Infra.Persistence.EF;

using Entities;
using Microsoft.EntityFrameworkCore;

public sealed class NorthwindDbContext : DbContext
{
  public NorthwindDbContext(DbContextOptions<NorthwindDbContext> optionsParam) : base(optionsParam) { }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Employee> Employees { get; set; }
  public DbSet<OrderDetail> OrderDetails { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Shipper> Shippers { get; set; }
  public DbSet<Supplier> Suppliers { get; set; }

  /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilderParam)
  {
    base.OnConfiguring(optionsBuilderParam);
  }*/

  protected override void OnModelCreating(ModelBuilder modelBuilderParam)
  {
    base.OnModelCreating(modelBuilderParam);
    modelBuilderParam.Entity<Category>().Property(c => c.CategoryName).IsRequired().HasMaxLength(15);
    // define a one-to-many relationship
    modelBuilderParam.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category);
    modelBuilderParam.Entity<Customer>().Property(c => c.CustomerID).IsRequired().HasMaxLength(5);
    modelBuilderParam.Entity<Customer>().Property(c => c.CompanyName).IsRequired().HasMaxLength(40);
    modelBuilderParam.Entity<Customer>().Property(c => c.ContactName).HasMaxLength(30);
    modelBuilderParam.Entity<Customer>().Property(c => c.Country).HasMaxLength(15);
    // define a one-to-many relationship
    modelBuilderParam.Entity<Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer);
    modelBuilderParam.Entity<Employee>().Property(c => c.LastName).IsRequired().HasMaxLength(20);
    modelBuilderParam.Entity<Employee>().Property(c => c.FirstName).IsRequired().HasMaxLength(10);
    modelBuilderParam.Entity<Employee>().Property(c => c.Country).HasMaxLength(15);
    // define a one-to-many relationship
    modelBuilderParam.Entity<Employee>().HasMany(e => e.Orders).WithOne(o => o.Employee);
    modelBuilderParam.Entity<Product>().Property(c => c.ProductName).IsRequired().HasMaxLength(40);
    modelBuilderParam.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products);
    modelBuilderParam.Entity<Product>().HasOne(p => p.Supplier).WithMany(s => s.Products);
    // define a one-to-many relationship
    // with a property key that does not
    // follow naming conventions
    modelBuilderParam.Entity<Order>().HasOne(o => o.Shipper).WithMany(s => s.Orders).HasForeignKey(o => o.ShipVia);
    // the table name has a space in it
    modelBuilderParam.Entity<OrderDetail>().ToTable("Order Details");
    // define multi-column primary key
    // for Order Details table
    modelBuilderParam.Entity<OrderDetail>().HasKey(od => new { od.OrderID, od.ProductID });
    modelBuilderParam.Entity<Supplier>().Property(c => c.CompanyName).IsRequired().HasMaxLength(40);
    modelBuilderParam.Entity<Supplier>().HasMany(s => s.Products).WithOne(p => p.Supplier);
  }
}