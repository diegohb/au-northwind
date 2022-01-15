namespace Infra.Persistence.EF.Entities;

using System;

public class SalesTotalsByAmountView
{
  public string CompanyName { get; set; }
  public int OrderId { get; set; }
  public decimal? SaleAmount { get; set; }
  public DateTime? ShippedDate { get; set; }
}