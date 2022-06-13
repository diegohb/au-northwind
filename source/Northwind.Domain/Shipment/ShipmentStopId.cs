namespace Northwind.Domain.Shipment;

using Catalog.Product;
using Core.Domain;

public class ShipmentStopId : IIdentityValueObject
{
  private const string idAsStringPrefix = "ShipmentStop-";

  private ShipmentStopId(int idParam)
  {
    Id = idParam;
  }

  public ShipmentStopId(string idParam)
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

  public static ShipmentStopId NewProductId(int productIdParam)
  {
    return new ShipmentStopId(productIdParam);
  }

  public static bool operator ==(ShipmentStopId left, ShipmentStopId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(ShipmentStopId left, ShipmentStopId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}