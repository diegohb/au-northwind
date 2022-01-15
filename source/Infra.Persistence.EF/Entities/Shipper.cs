namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class Shipper
{
  public Shipper()
  {
    Orders = new HashSet<Order>();
  }

  public string CompanyName { get; set; }

  public virtual ICollection<Order> Orders { get; set; }
  public string Phone { get; set; }

  public int ShipperId { get; set; }
}