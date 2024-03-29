﻿namespace Northwind.Domain.Shipment;

using Catalog.Product;
using Core.Domain;

public class ShipmentId : IIdentityValueObject
{
  private const string idAsStringPrefix = "Shipment-";

  private ShipmentId(int idParam)
  {
    Id = idParam;
  }

  public ShipmentId(string idParam)
  {
    if (string.IsNullOrEmpty(idParam))
    {
      throw new ArgumentNullException(nameof(idParam));
    }

    Id = int.Parse(idParam.StartsWith(idAsStringPrefix) ? idParam.Substring(idAsStringPrefix.Length) : idParam);
  }

  public int Id { get; }

  public string IdAsString()
  {
    return $"{idAsStringPrefix}{Id.ToString()}";
  }

  public override bool Equals(object? objParam)
  {
    return objParam is ProductId id && Equals(Id, id.Id);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public static ShipmentId NewProductId(int productIdParam)
  {
    return new ShipmentId(productIdParam);
  }

  public static bool operator ==(ShipmentId left, ShipmentId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(ShipmentId left, ShipmentId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}