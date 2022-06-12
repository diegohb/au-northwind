namespace Northwind.Core.Persistence.EventStore;

using Domain;

public class Event<TAggregateId>
{
  public Event(IDomainEvent<TAggregateId> domainEvent, long eventNumber)
  {
    DomainEvent = domainEvent;
    EventNumber = eventNumber;
  }

  public IDomainEvent<TAggregateId> DomainEvent { get; }

  public long EventNumber { get; }
}