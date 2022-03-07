namespace Northwind.Core.Persistence;

using Domain;

internal interface IEventSourcingAggregate<TAggregateId>
{
  public long Version { get; }

  internal void ApplyEvent(IDomainEvent<TAggregateId> @event, long version);

  public void ClearUncommittedEvents();

  IEnumerable<IDomainEvent<TAggregateId>> GetUncommittedEvents();
}