namespace Infra.Persistence.EF;

using System.Collections.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;

public class NorthwindDbContext : DbContext
{
  public NorthwindDbContext(DbContextOptions<NorthwindDbContext> optionsParam) : base(optionsParam) { }

  /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilderParam)
  {
    base.OnConfiguring(optionsBuilderParam);
  }*/

  protected override void OnModelCreating(ModelBuilder modelBuilderParam)
  {
    base.OnModelCreating(modelBuilderParam);

    modelBuilderParam.ApplyConfigurationsFromAssembly(typeof(AlphabeticalListOfProductView).Assembly);

    modelBuilderParam.Entity<Category>
    (entity =>
    {
      entity.HasIndex(e => e.CategoryName, "CategoryName");

      entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

      entity.Property(e => e.CategoryName)
        .IsRequired()
        .HasMaxLength(15);

      entity.Property(e => e.Description).HasColumnType("ntext");

      entity.Property(e => e.Picture).HasColumnType("image");

      entity.HasMany(e => e.Products)
        .WithOne(e => e.Category)
        .HasForeignKey(e => e.CategoryId)
        .HasConstraintName("FK_Products_Categories");
    });

    modelBuilderParam.Entity<Customer>
    (entity =>
    {
      entity.HasIndex(e => e.City, "City");

      entity.HasIndex(e => e.CompanyName, "CompanyName");

      entity.HasIndex(e => e.PostalCode, "PostalCode");

      entity.HasIndex(e => e.Region, "Region");

      entity.Property(e => e.CustomerId)
        .HasMaxLength(5)
        .HasColumnName("CustomerID")
        .IsFixedLength();

      entity.Property(e => e.Address).HasMaxLength(60);

      entity.Property(e => e.City).HasMaxLength(15);

      entity.Property(e => e.CompanyName)
        .IsRequired()
        .HasMaxLength(40);

      entity.Property(e => e.ContactName).HasMaxLength(30);

      entity.Property(e => e.ContactTitle).HasMaxLength(30);

      entity.Property(e => e.Country).HasMaxLength(15);

      entity.Property(e => e.Fax).HasMaxLength(24);

      entity.Property(e => e.Phone).HasMaxLength(24);

      entity.Property(e => e.PostalCode).HasMaxLength(10);

      entity.Property(e => e.Region).HasMaxLength(15);

      entity.HasMany(e => e.Orders)
        .WithOne(e => e.Customer)
        .HasForeignKey(e => e.CustomerId)
        .HasConstraintName("FK_Orders_Customers");
    });

    modelBuilderParam.Entity<CustomerDemographic>
    (entity =>
    {
      entity.HasKey(e => e.CustomerTypeId)
        .IsClustered(false);

      entity.Property(e => e.CustomerTypeId)
        .HasMaxLength(10)
        .HasColumnName("CustomerTypeID")
        .IsFixedLength();

      entity.Property(e => e.CustomerDesc).HasColumnType("ntext");
    });

    modelBuilderParam.Entity<Employee>
    (entity =>
    {
      entity.HasIndex(e => e.LastName, "LastName");

      entity.HasIndex(e => e.PostalCode, "PostalCode");

      entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

      entity.Property(e => e.Address).HasMaxLength(60);

      entity.Property(e => e.BirthDate).HasColumnType("datetime");

      entity.Property(e => e.City).HasMaxLength(15);

      entity.Property(e => e.Country).HasMaxLength(15);

      entity.Property(e => e.Extension).HasMaxLength(4);

      entity.Property(e => e.FirstName)
        .IsRequired()
        .HasMaxLength(10);

      entity.Property(e => e.HireDate).HasColumnType("datetime");

      entity.Property(e => e.HomePhone).HasMaxLength(24);

      entity.Property(e => e.LastName)
        .IsRequired()
        .HasMaxLength(20);

      entity.Property(e => e.Notes).HasColumnType("ntext");

      entity.Property(e => e.Photo).HasColumnType("image");

      entity.Property(e => e.PhotoPath).HasMaxLength(255);

      entity.Property(e => e.PostalCode).HasMaxLength(10);

      entity.Property(e => e.Region).HasMaxLength(15);

      entity.Property(e => e.Title).HasMaxLength(30);

      entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

      entity.HasMany(d => d.Territories)
        .WithMany(p => p.Employees)
        .UsingEntity<Dictionary<string, object>>
        (
          "EmployeeTerritory",
          l => l.HasOne<Territory>()
            .WithMany()
            .HasForeignKey("TerritoryId")
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName
              ("FK_EmployeeTerritories_Territories"),
          r => r.HasOne<Employee>()
            .WithMany()
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName
              ("FK_EmployeeTerritories_Employees"),
          j =>
          {
            j.HasKey("EmployeeId", "TerritoryId").IsClustered(false);

            j.ToTable("EmployeeTerritories");

            j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");

            j.IndexerProperty<string>("TerritoryId").HasMaxLength(20).HasColumnName("TerritoryID");
          });

      entity.HasMany(e => e.Orders)
        .WithOne(e => e.Employee)
        .HasForeignKey(e => e.EmployeeId)
        .HasConstraintName("FK_Orders_Employees");
    });

    modelBuilderParam.Entity<Order>
    (entity =>
    {
      entity.HasIndex(e => e.CustomerId, "CustomerID");

      entity.HasIndex(e => e.CustomerId, "CustomersOrders");

      entity.HasIndex(e => e.EmployeeId, "EmployeeID");

      entity.HasIndex(e => e.EmployeeId, "EmployeesOrders");

      entity.HasIndex(e => e.OrderDate, "OrderDate");

      entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

      entity.HasIndex(e => e.ShippedDate, "ShippedDate");

      entity.HasIndex(e => e.ShipVia, "ShippersOrders");

      entity.Property(e => e.OrderId).HasColumnName("OrderID");

      entity.Property(e => e.CustomerId)
        .HasMaxLength(5)
        .HasColumnName("CustomerID")
        .IsFixedLength();

      entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

      entity.Property(e => e.Freight)
        .HasColumnType("money")
        .HasDefaultValueSql("((0))");

      entity.Property(e => e.OrderDate).HasColumnType("datetime");

      entity.Property(e => e.RequiredDate).HasColumnType("datetime");

      entity.Property(e => e.ShipAddress).HasMaxLength(60);

      entity.Property(e => e.ShipCity).HasMaxLength(15);

      entity.Property(e => e.ShipCountry).HasMaxLength(15);

      entity.Property(e => e.ShipName).HasMaxLength(40);

      entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

      entity.Property(e => e.ShipRegion).HasMaxLength(15);

      entity.Property(e => e.ShippedDate).HasColumnType("datetime");

      entity.HasOne(d => d.Customer)
        .WithMany(p => p.Orders)
        .HasForeignKey(d => d.CustomerId)
        .HasConstraintName("FK_Orders_Customers");

      entity.HasOne(d => d.Employee)
        .WithMany(p => p.Orders)
        .HasForeignKey(d => d.EmployeeId)
        .HasConstraintName("FK_Orders_Employees");

      entity.HasOne(d => d.Shipper)
        .WithMany(p => p.Orders)
        .HasForeignKey(d => d.ShipVia)
        .HasConstraintName("FK_Orders_Shippers");
    });

    modelBuilderParam.Entity<OrderDetail>
    (entity =>
    {
      entity.HasKey(e => new { e.OrderId, e.ProductId })
        .HasName("PK_Order_Details");

      entity.ToTable("Order Details");

      entity.HasIndex(e => e.OrderId, "OrderID");

      entity.HasIndex(e => e.OrderId, "OrdersOrder_Details");

      entity.HasIndex(e => e.ProductId, "ProductID");

      entity.HasIndex(e => e.ProductId, "ProductsOrder_Details");

      entity.Property(e => e.OrderId).HasColumnName("OrderID");

      entity.Property(e => e.ProductId).HasColumnName("ProductID");

      entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

      entity.Property(e => e.UnitPrice).HasColumnType("money");

      entity.HasOne(d => d.Order)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Order_Details_Orders");

      entity.HasOne(d => d.Product)
        .WithMany(p => p.OrderDetails)
        .HasForeignKey(d => d.ProductId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Order_Details_Products");
    });

    modelBuilderParam.Entity<Product>
    (entity =>
    {
      entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

      entity.HasIndex(e => e.CategoryId, "CategoryID");

      entity.HasIndex(e => e.ProductName, "ProductName");

      entity.HasIndex(e => e.SupplierId, "SupplierID");

      entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

      entity.Property(e => e.ProductId).HasColumnName("ProductID");

      entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

      entity.Property(e => e.ProductName)
        .IsRequired()
        .HasMaxLength(40);

      entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

      entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

      entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

      entity.Property(e => e.UnitPrice)
        .HasColumnType("money")
        .HasDefaultValueSql("((0))");

      entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

      entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

      entity.HasOne(d => d.Category)
        .WithMany(p => p.Products)
        .HasForeignKey(d => d.CategoryId)
        .HasConstraintName("FK_Products_Categories");

      entity.HasOne(d => d.Supplier)
        .WithMany(p => p.Products)
        .HasForeignKey(d => d.SupplierId)
        .HasConstraintName("FK_Products_Suppliers");

      //entity.Navigation(e => e.OrderDetails).AutoInclude(false); //NOTE: to not automatically eager load related data
    });

    modelBuilderParam.Entity<Region>
    (entity =>
    {
      entity.HasKey(e => e.RegionId)
        .IsClustered(false);

      entity.ToTable("Region");

      entity.Property(e => e.RegionId)
        .ValueGeneratedNever()
        .HasColumnName("RegionID");

      entity.Property(e => e.RegionDescription)
        .IsRequired()
        .HasMaxLength(50)
        .IsFixedLength();
    });

    modelBuilderParam.Entity<Shipper>
    (entity =>
    {
      entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

      entity.Property(e => e.CompanyName)
        .IsRequired()
        .HasMaxLength(40);

      entity.Property(e => e.Phone).HasMaxLength(24);
    });

    modelBuilderParam.Entity<Supplier>
    (entity =>
    {
      entity.HasIndex(e => e.CompanyName, "CompanyName");

      entity.HasIndex(e => e.PostalCode, "PostalCode");

      entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

      entity.Property(e => e.Address).HasMaxLength(60);

      entity.Property(e => e.City).HasMaxLength(15);

      entity.Property(e => e.CompanyName)
        .IsRequired()
        .HasMaxLength(40);

      entity.Property(e => e.ContactName).HasMaxLength(30);

      entity.Property(e => e.ContactTitle).HasMaxLength(30);

      entity.Property(e => e.Country).HasMaxLength(15);

      entity.Property(e => e.Fax).HasMaxLength(24);

      entity.Property(e => e.HomePage).HasColumnType("ntext");

      entity.Property(e => e.Phone).HasMaxLength(24);

      entity.Property(e => e.PostalCode).HasMaxLength(10);

      entity.Property(e => e.Region).HasMaxLength(15);
    });

    modelBuilderParam.Entity<Territory>
    (entity =>
    {
      entity.HasKey(e => e.TerritoryId)
        .IsClustered(false);

      entity.Property(e => e.TerritoryId)
        .HasMaxLength(20)
        .HasColumnName("TerritoryID");

      entity.Property(e => e.RegionId).HasColumnName("RegionID");

      entity.Property(e => e.TerritoryDescription)
        .IsRequired()
        .HasMaxLength(50)
        .IsFixedLength();

      entity.HasOne(d => d.Region)
        .WithMany(p => p.Territories)
        .HasForeignKey(d => d.RegionId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Territories_Region");
    });
  }

  #region Properties

  public virtual DbSet<AlphabeticalListOfProductView> AlphabeticalListOfProducts { get; set; }
  public virtual DbSet<Category> Categories { get; set; }
  public virtual DbSet<CategorySalesFor1997View> CategorySalesFor1997s { get; set; }
  public virtual DbSet<CurrentProductListView> CurrentProductLists { get; set; }
  public virtual DbSet<CustomerAndSuppliersByCityView> CustomerAndSuppliersByCities { get; set; }
  public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
  public virtual DbSet<Customer> Customers { get; set; }
  public virtual DbSet<Employee> Employees { get; set; }
  public virtual DbSet<InvoiceView> Invoices { get; set; }
  public virtual DbSet<OrderDetail> OrderDetails { get; set; }
  public virtual DbSet<OrderDetailsExtendedView> OrderDetailsExtendeds { get; set; }
  public virtual DbSet<Order> Orders { get; set; }
  public virtual DbSet<OrdersQryView> OrdersQries { get; set; }
  public virtual DbSet<OrderSubtotalView> OrderSubtotals { get; set; }
  public virtual DbSet<Product> Products { get; set; }
  public virtual DbSet<ProductsAboveAveragePriceView> ProductsAboveAveragePrices { get; set; }
  public virtual DbSet<ProductSalesFor1997View> ProductSalesFor1997s { get; set; }
  public virtual DbSet<ProductsByCategoryView> ProductsByCategories { get; set; }
  public virtual DbSet<QuarterlyOrderView> QuarterlyOrders { get; set; }
  public virtual DbSet<Region> Regions { get; set; }
  public virtual DbSet<SalesByCategoryView> SalesByCategories { get; set; }
  public virtual DbSet<SalesTotalsByAmountView> SalesTotalsByAmounts { get; set; }
  public virtual DbSet<Shipper> Shippers { get; set; }
  public virtual DbSet<SummaryOfSalesByQuarterView> SummaryOfSalesByQuarters { get; set; }
  public virtual DbSet<SummaryOfSalesByYearView> SummaryOfSalesByYears { get; set; }
  public virtual DbSet<Supplier> Suppliers { get; set; }
  public virtual DbSet<Territory> Territories { get; set; }

  #endregion
}