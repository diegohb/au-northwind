namespace Infra.Persistence.EF.Entities;

using System;

public class SummaryOfSalesByQuarterView
{
  public int OrderId { get; set; }
  public DateTime? ShippedDate { get; set; }
  public decimal? Subtotal { get; set; }
}