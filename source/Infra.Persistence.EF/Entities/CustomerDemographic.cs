namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class CustomerDemographic
{
  public CustomerDemographic()
  {
    Customers = new HashSet<Customer>();
  }

  public string CustomerDesc { get; set; }

  public virtual ICollection<Customer> Customers { get; set; }

  public string CustomerTypeId { get; set; }
}