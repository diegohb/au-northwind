﻿namespace Northwind.Core.Domain;

/// <summary>
///   Base class for value objects in domain.
///   Value
/// </summary>
/// <typeparam name="TValueObject">The type of this value object</typeparam>
public abstract class ValueObjectBase<TValueObject> : IEquatable<TValueObject>
  where TValueObject : ValueObjectBase<TValueObject>
{
  #region IEquatable and Override Equals operators

  /// <summary>
  ///   <see cref="M:System.Object.IEquatable{TValueObject}" />
  /// </summary>
  /// <param name="otherParam">
  ///   <see cref="M:System.Object.IEquatable{TValueObject}" />
  /// </param>
  /// <returns>
  ///   <see cref="M:System.Object.IEquatable{TValueObject}" />
  /// </returns>
  public virtual bool Equals(TValueObject? otherParam)
  {
    if (otherParam is null)
    {
      return false;
    }

    if (ReferenceEquals(this, otherParam))
    {
      return true;
    }

    //compare all public properties
    var publicProperties = GetType().GetProperties();

    if (publicProperties.Any())
    {
      return publicProperties.All
      (p =>
      {
        var left = p.GetValue(this, null);
        var right = p.GetValue(otherParam, null);

        return left != null && (left is TValueObject ? ReferenceEquals(left, right) : left.Equals(right));
      });
    }

    return true;
  }

  /// <summary>
  ///   <see cref="M:System.Object.Equals" />
  /// </summary>
  /// <param name="objParam">
  ///   <see cref="M:System.Object.Equals" />
  /// </param>
  /// <returns>
  ///   <see cref="M:System.Object.Equals" />
  /// </returns>
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

    return objParam is ValueObjectBase<TValueObject> item && Equals((TValueObject)item);
  }

  /// <summary>
  ///   <see cref="M:System.Object.GetHashCode" />
  /// </summary>
  /// <returns>
  ///   <see cref="M:System.Object.GetHashCode" />
  /// </returns>
  public override int GetHashCode()
  {
    var hashCode = 31;
    var changeMultiplier = false;
    const int index = 1;

    //compare all public properties
    var publicProperties = GetType().GetProperties();

    if (!publicProperties.Any())
    {
      return hashCode;
    }

    foreach (var value in publicProperties.Select(item => item.GetValue(this, null)))
    {
      if (value != null)
      {
        hashCode = hashCode * (changeMultiplier ? 59 : 114) + value.GetHashCode();

        changeMultiplier = !changeMultiplier;
      }
      else
      {
        hashCode = hashCode ^ (index * 13); //only for support {"a",null,null,"a"} <> {null,"a","a",null}
      }
    }

    return hashCode;
  }

  public static bool operator ==(ValueObjectBase<TValueObject>? left, ValueObjectBase<TValueObject>? right)
  {
    return left?.Equals(right) ?? false;
  }

  public static bool operator !=(ValueObjectBase<TValueObject>? left, ValueObjectBase<TValueObject>? right)
  {
    return !(left == right);
  }

  #endregion
}