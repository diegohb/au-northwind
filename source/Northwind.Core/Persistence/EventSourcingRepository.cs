﻿namespace Northwind.Core.Persistence;

using System.Reflection;
using Domain;
using EventStore;

public class EventSourcingRepository<TAggregate, TAggregateId> : IAggregateRepository<TAggregate, TAggregateId>
  where TAggregate : AggregateBase<TAggregateId>, IHaveIdentity<TAggregateId>
  where TAggregateId : IIdentityValueObject
{
  private readonly IEventStore _eventStore;
  private readonly IDomainMediator<TAggregateId>? _publisher;

  public EventSourcingRepository(IEventStore eventStore, IDomainMediator<TAggregateId> publisher)
  {
    _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
    _publisher = publisher;
  }

  public async Task<TAggregate?> GetByIdAsync(TAggregateId id)
  {
    try
    {
      var aggregate = createEmptyAggregate();
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

        if (_publisher is not null)
        {
          await _publisher.PublishAsync((dynamic)@event);
        }
      }

      aggregatePersistence.ClearUncommittedEvents();
    }
    catch (EventStoreCommunicationException ex)
    {
      throw new RepositoryException("Unable to access persistence layer", ex);
    }
  }

  #region Support Methods

  private TAggregate createEmptyAggregate()
  {
    return (TAggregate)typeof(TAggregate)
      .GetConstructor
      (BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
        null, Type.EmptyTypes, Array.Empty<ParameterModifier>())!
      .Invoke(Array.Empty<object>());
  }

  #endregion
}