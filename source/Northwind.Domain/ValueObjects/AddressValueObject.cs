namespace Northwind.Domain.ValueObjects;

using Core.Domain;

public class AddressValueObject : ValueObjectBase<AddressValueObject>
{
  public AddressValueObject(string streetAddress, string city, string state, string zip)
  {
    StreetAddress = streetAddress;
    City = city;
    State = state;
    Zip = zip;
  }

  public string City { get; }
  public string State { get; }

  public string StreetAddress { get; }
  public string Zip { get; }

  #region Equality

  public override bool Equals(AddressValueObject? other)
  {
    if (ReferenceEquals(null, other))
    {
      return false;
    }

    if (ReferenceEquals(this, other))
    {
      return true;
    }

    return base.Equals(other) && string.Equals(StreetAddress, other.StreetAddress) && string.Equals(City, other.City) && Equals
      (State, other.State) && string.Equals(Zip, other.Zip);
  }

  public override int GetHashCode()
  {
    unchecked
    {
      var hashCode = base.GetHashCode();
      hashCode = (hashCode * 397) ^ (StreetAddress != null ? StreetAddress.GetHashCode() : 0);
      hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
      hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
      hashCode = (hashCode * 397) ^ (Zip != null ? Zip.GetHashCode() : 0);
      return hashCode;
    }
  }

  #endregion equality
}