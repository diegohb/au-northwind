namespace Northwind.Core.Domain;

public interface IHaveIdentity<TId>
  where TId : IIdentityValueObject
{
  TId Id { get; }
}