﻿namespace Infra.Persistence.EF.Entities;

public class CustomerAndSuppliersByCityView
{
  public string City { get; set; }
  public string CompanyName { get; set; }
  public string ContactName { get; set; }
  public string Relationship { get; set; }
}