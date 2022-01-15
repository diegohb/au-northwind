namespace Infra.Persistence.EF.Entities;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class Category
{
  public Category()
  {
    Products = new HashSet<Product>();
  }

  public int CategoryId { get; set; }
  public string CategoryName { get; set; }
  public string Description { get; set; }
  public byte[] Picture { get; set; }

  public virtual ICollection<Product> Products { get; set; }
}

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    throw new NotImplementedException();
  }
}