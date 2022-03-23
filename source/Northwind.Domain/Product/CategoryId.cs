namespace Northwind.Domain.Product;

using Core.Domain;

public class CategoryId : IIdentityValueObject
{
  private const string idAsStringPrefix = "Category-";

  private CategoryId(int idParam)
  {
    Id = idParam;
  }

  public CategoryId(string idParam)
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
    return objParam is CategoryId id && Equals(Id, id.Id);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }

  public static CategoryId NewCategoryId(int categoryIdParam)
  {
    return new CategoryId(categoryIdParam);
  }

  public static bool operator ==(CategoryId left, CategoryId? right)
  {
    return Equals(left?.Id, right?.Id);
  }

  public static bool operator !=(CategoryId left, CategoryId? right)
  {
    return !(left == right);
  }

  public override string ToString()
  {
    return IdAsString();
  }
}