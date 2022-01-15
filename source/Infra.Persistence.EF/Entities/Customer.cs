﻿namespace Infra.Persistence.EF.Entities
{
  using System.Collections.Generic;

  public class Customer
  {
    public string Address { get; set; }
    public string City { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Country { get; set; }
    public string CustomerID { get; set; }
    public string Fax { get; set; }

    // related entities
    public ICollection<Order> Orders { get; set; }
    public string Phone { get; set; }
    public string PostalCode { get; set; }
    public string Region { get; set; }
  }
}