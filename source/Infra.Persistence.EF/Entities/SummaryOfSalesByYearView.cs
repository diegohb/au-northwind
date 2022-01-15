namespace Infra.Persistence.EF.Entities;

using System;

public class SummaryOfSalesByYearView
{
  public int OrderId { get; set; }
  public DateTime? ShippedDate { get; set; }
  public decimal? Subtotal { get; set; }
}