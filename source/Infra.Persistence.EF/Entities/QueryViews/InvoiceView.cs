﻿namespace Infra.Persistence.EF.Entities.QueryViews;

using System;

public class InvoiceView
{
  public string Address { get; set; }
  public string City { get; set; }
  public string Country { get; set; }
  public string CustomerId { get; set; }
  public string CustomerName { get; set; }
  public float Discount { get; set; }
  public decimal? ExtendedPrice { get; set; }
  public decimal? Freight { get; set; }
  public DateTime? OrderDate { get; set; }
  public int OrderId { get; set; }
  public string PostalCode { get; set; }
  public int ProductId { get; set; }
  public string ProductName { get; set; }
  public short Quantity { get; set; }
  public string Region { get; set; }
  public DateTime? RequiredDate { get; set; }
  public string Salesperson { get; set; }
  public string ShipAddress { get; set; }
  public string ShipCity { get; set; }
  public string ShipCountry { get; set; }
  public string ShipName { get; set; }
  public DateTime? ShippedDate { get; set; }
  public string ShipperName { get; set; }
  public string ShipPostalCode { get; set; }
  public string ShipRegion { get; set; }
  public Guid Sku { get; set; }
  public decimal UnitPrice { get; set; }
}