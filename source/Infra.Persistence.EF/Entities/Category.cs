namespace Infra.Persistence.EF.Entities;

using System.Collections.Generic;

public class Category
{
  public Category()
  {
    Products = new HashSet<Product>();
  }

  public int CategoryId { get; private set; }
  public string CategoryName { get; private set; }
  public string Description { get; private set; }
  public byte[] Picture { get; private set; }

  public ICollection<Product> Products { get; }
}