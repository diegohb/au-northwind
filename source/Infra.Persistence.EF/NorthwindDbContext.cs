namespace Infra.Persistence.EF;

using Entities;
using Entities.QueryViews;
using MapConfigs;
using Microsoft.EntityFrameworkCore;

public class NorthwindDbContext : DbContext
{
  public NorthwindDbContext() { }

  public NorthwindDbContext(DbContextOptions<NorthwindDbContext> optionsParam)
    : base(optionsParam) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilderParam)
  {
    base.OnConfiguring(optionsBuilderParam);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilderParam)
  {
    base.OnModelCreating(modelBuilderParam);

    modelBuilderParam.ApplyConfigurationsFromAssembly(typeof(ProductMapping).Assembly);
  }

  #region Query View Properties

  public virtual DbSet<AlphabeticalListOfProductView> AlphabeticalListOfProducts { get; set; }
  public virtual DbSet<CategorySalesFor1997View> CategorySalesFor1997s { get; set; }
  public virtual DbSet<CurrentProductListView> CurrentProductLists { get; set; }
  public virtual DbSet<CustomerAndSuppliersByCityView> CustomerAndSuppliersByCities { get; set; }
  public virtual DbSet<OrderDetailsExtendedView> OrderDetailsExtendeds { get; set; }
  public virtual DbSet<OrdersQryView> OrdersQries { get; set; }
  public virtual DbSet<OrderSubtotalView> OrderSubtotals { get; set; }
  public virtual DbSet<ProductsAboveAveragePriceView> ProductsAboveAveragePrices { get; set; }
  public virtual DbSet<ProductSalesFor1997View> ProductSalesFor1997s { get; set; }
  public virtual DbSet<ProductsByCategoryView> ProductsByCategories { get; set; }
  public virtual DbSet<QuarterlyOrderView> QuarterlyOrders { get; set; }
  public virtual DbSet<SalesByCategoryView> SalesByCategories { get; set; }
  public virtual DbSet<SalesTotalsByAmountView> SalesTotalsByAmounts { get; set; }
  public virtual DbSet<SummaryOfSalesByQuarterView> SummaryOfSalesByQuarters { get; set; }
  public virtual DbSet<SummaryOfSalesByYearView> SummaryOfSalesByYears { get; set; }

  #endregion

  #region Entity Properties

  public virtual DbSet<InvoiceView> Invoices { get; set; }
  public virtual DbSet<Category> Categories { get; set; }
  public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
  public virtual DbSet<Customer> Customers { get; set; }
  public virtual DbSet<Employee> Employees { get; set; }
  public virtual DbSet<OrderDetail> OrderDetails { get; set; }
  public virtual DbSet<Order> Orders { get; set; }
  public virtual DbSet<Product> Products { get; set; }
  public virtual DbSet<Region> Regions { get; set; }
  public virtual DbSet<Shipper> Shippers { get; set; }
  public virtual DbSet<Supplier> Suppliers { get; set; }
  public virtual DbSet<Territory> Territories { get; set; }

  #endregion
}