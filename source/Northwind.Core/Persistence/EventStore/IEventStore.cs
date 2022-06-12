﻿namespace Northwind.Core.Persistence.EventStore;

using Domain;

public interface IEventStore
{
  Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
    where TAggregateId : IIdentityValueObject;

  Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
    where TAggregateId : IIdentityValueObject;
}