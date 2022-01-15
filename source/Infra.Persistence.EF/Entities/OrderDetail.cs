namespace Infra.Persistence.EF.Entities;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderDetail
{
  public float Discount { get; set; }

  public virtual Order Order { get; set; }
  public int OrderId { get; set; }
  public virtual Product Product { get; set; }
  public int ProductId { get; set; }
  public short Quantity { get; set; }
  public decimal UnitPrice { get; set; }
}

public class OrderDetailMapping : IEntityTypeConfiguration<OrderDetail>
{
  public void Configure(EntityTypeBuilder<OrderDetail> builder)
  {
    throw new NotImplementedException();
  }
}