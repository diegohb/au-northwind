namespace Northwind.Core.Domain;

public interface IAggregate<TId>
{
  TId Id { get; }
}