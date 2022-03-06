namespace SharedKernel.Persistence;

using Domain;

internal interface IEventSourcingAggregate<TAggregateId>
{
  long Version { get; }

  void ApplyEvent(IDomainEvent<TAggregateId> @event, long version);

  void ClearUncommittedEvents();

  IEnumerable<IDomainEvent<TAggregateId>> GetUncommittedEvents();
}