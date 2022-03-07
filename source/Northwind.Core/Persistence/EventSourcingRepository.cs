namespace Northwind.Core.Persistence;

using System.Reflection;
using Domain;
using EventStore;

public class EventSourcingRepository<TAggregate, TAggregateId> : IRepository<TAggregate, TAggregateId>
  where TAggregate : AggregateBase<TAggregateId>, IAggregate<TAggregateId>
  where TAggregateId : IAggregateId
{
  private readonly IEventStore _eventStore;
  private readonly IDomainMediator<TAggregateId> _publisher;

  public EventSourcingRepository(IEventStore eventStore, IDomainMediator<TAggregateId> publisher)
  {
    _eventStore = eventStore;
    _publisher = publisher;
  }

  public async Task<TAggregate?> GetByIdAsync(TAggregateId id)
  {
    try
    {
      var aggregate = CreateEmptyAggregate();
      IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

      foreach (var @event in await _eventStore.ReadEventsAsync(id))
      {
        aggregatePersistence.ApplyEvent(@event.DomainEvent, @event.EventNumber);
      }

      return aggregate;
    }
    catch (EventStoreAggregateNotFoundException)
    {
      return null;
    }
    catch (EventStoreCommunicationException ex)
    {
      throw new RepositoryException("Unable to access persistence layer", ex);
    }
  }

  public async Task SaveAsync(TAggregate aggregate)
  {
    try
    {
      IEventSourcingAggregate<TAggregateId> aggregatePersistence = aggregate;

      foreach (var @event in aggregatePersistence.GetUncommittedEvents())
      {
        await _eventStore.AppendEventAsync(@event);
        await _publisher.PublishAsync((dynamic)@event);
      }

      aggregatePersistence.ClearUncommittedEvents();
    }
    catch (EventStoreCommunicationException ex)
    {
      throw new RepositoryException("Unable to access persistence layer", ex);
    }
  }

  #region Support Methods

  private TAggregate CreateEmptyAggregate()
  {
    return (TAggregate)typeof(TAggregate)
      .GetConstructor
      (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
        null, Type.EmptyTypes, Array.Empty<ParameterModifier>())!
      .Invoke(Array.Empty<object>());
  }

  #endregion
}