namespace Northwind.Domain.Catalog;

using Core.Domain;

public class ProductId : IIdentityValueObject
{
  private const string idAsStringPrefix = "Product-";

  private ProductId(int idParam)
  {
    Id = idParam;
  }

  public ProductId(string idParam)
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

  public static ProductId NewProductId(int productIdParam)
  {
    return new ProductId(productIdParam);
  }

  public static bool operator ==(ProductId left, ProductId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(ProductId left, ProductId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}