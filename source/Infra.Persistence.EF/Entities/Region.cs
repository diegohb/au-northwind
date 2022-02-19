namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class Region
{
  public Region()
  {
    Territories = new HashSet<Territory>();
  }

  public string RegionDescription { get; set; }

  public int RegionId { get; set; }

  public virtual ICollection<Territory> Territories { get; set; }
}