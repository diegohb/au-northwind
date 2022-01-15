namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class Territory
{
  public Territory()
  {
    Employees = new HashSet<Employee>();
  }

  public virtual ICollection<Employee> Employees { get; set; }

  public virtual Region Region { get; set; }
  public int RegionId { get; set; }
  public string TerritoryDescription { get; set; }

  public string TerritoryId { get; set; }
}