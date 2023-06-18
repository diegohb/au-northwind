namespace Northwind.Core.Domain;

public interface IHaveIdentity<out TId>
  where TId : IIdentityValueObject
{
  TId Id { get; }
}