namespace Infra.Persistence.EF.Entities
{
  using System.Collections.Generic;

  public class Shipper
  {
    // related entities
    public ICollection<Order> Orders { get; set; }
    public string Phone { get; set; }
    public int ShipperID { get; set; }
    public string ShipperName { get; set; }
  }
}