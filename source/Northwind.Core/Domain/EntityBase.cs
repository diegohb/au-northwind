namespace Northwind.Core.Domain;

using System.Diagnostics;

public abstract class EntityBase<TIdentity> : IEquatable<EntityBase<TIdentity>>
  where TIdentity : IIdentityValueObject
{
  protected EntityBase(TIdentity idParam)
  {
    Id = idParam;
  }

  protected EntityBase() { }

  public TIdentity Id { get; protected set; } = default!;

  public static bool operator ==(EntityBase<TIdentity>? leftParam, EntityBase<TIdentity>? rightParam)
  {
    if (leftParam is null && rightParam is null)
    {
      return true;
    }

    if (leftParam is null || rightParam is null)
    {
      return false;
    }

    return leftParam.Equals(rightParam);
  }

  public static bool operator !=(EntityBase<TIdentity>? leftParam, EntityBase<TIdentity>? rightParam)
  {
    return !(leftParam == rightParam);
  }

  #region Overrides of Object

  public override string ToString()
  {
    return $"{GetType().Name}#[Identity={Id}]";
  }

  #endregion


  #region Implementation of IEquatable<Entity>

  public bool Equals(EntityBase<TIdentity>? otherParam)
  {
    if (otherParam is null)
    {
      return false;
    }

    if (ReferenceEquals(this, otherParam))
    {
      return true;
    }

    return Equals(Id, otherParam.Id);
  }

  public override bool Equals(object? objParam)
  {
    if (objParam is null)
    {
      return false;
    }

    if (ReferenceEquals(this, objParam))
    {
      return true;
    }

    if (objParam.GetType() != GetType())
    {
      return false;
    }

    return Equals((EntityBase<TIdentity>)objParam);
  }

  public override int GetHashCode()
  {
    Debug.Assert(Id != null, nameof(Id) + " != null");
    return GetType().GetHashCode() * 907 + Id.GetHashCode();
  }

  #endregion
}