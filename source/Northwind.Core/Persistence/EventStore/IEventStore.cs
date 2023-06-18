namespace Northwind.Core.Persistence.EventStore;

using Domain;

public interface IEventStore
{
  Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> eventParam)
    where TAggregateId : IIdentityValueObject;

  Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId idParam)
    where TAggregateId : IIdentityValueObject;
}