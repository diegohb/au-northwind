namespace Northwind.UnitTests.Catalog.Product;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Core.Persistence.EventStore;
using Domain.Catalog;

public class InMemProductEventStore : IEventStore
{
  private readonly HashSet<Event<ProductId>> _events = new();

  public Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> @event)
    where TAggregateId : IIdentityValueObject
  {
    _events.Add(new Event<ProductId>((IDomainEvent<ProductId>)@event, @event.AggregateVersion));
    return Task.FromResult(new AppendResult(@event.AggregateVersion + 1));
  }

  public Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId id)
    where TAggregateId : IIdentityValueObject
  {
    var results = _events.Where(ev => ev.DomainEvent.AggregateId.Equals(id));
    return Task.FromResult(results.Cast<Event<TAggregateId>>());
  }

  internal void ResetForTesting()
  {
    _events.Clear();
  }
}