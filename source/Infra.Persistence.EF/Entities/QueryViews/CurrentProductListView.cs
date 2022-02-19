namespace Infra.Persistence.EF.Entities.QueryViews;

using System;

public class CurrentProductListView
{
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public Guid Sku { get; set; }
}