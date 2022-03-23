namespace Northwind.UnitTests;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Persistence;
using Core.Persistence.EventStore;
using Domain.Product;
using NUnit.Framework;

[TestFixture]
public class ProductTestFixture
{
  private readonly InMemProductEventStore _eventStore = new();
  private readonly FakeProductIdMediator _productNotificationMediator = new();
  private EventSourcingRepository<CatalogProduct, ProductId> _productRepo = null!;

  private CatalogProduct _sut = null!;

  [SetUp]
  public void Setup()
  {
    _productRepo = new EventSourcingRepository<CatalogProduct, ProductId>(_eventStore, _productNotificationMediator);
  }

  [Test]
  public async Task SaveAndLoadAggregateShouldReturnAddedAggregate()
  {
    var newProductId = ProductId.NewProductId(101);
    var expectedProductGuid = Guid.NewGuid();
    _sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(_sut);
    var result = await _productRepo.GetByIdAsync(newProductId);
    Assert.IsNotNull(result);
    Assert.AreEqual(expectedProductGuid, result!.Sku);
    var messages = _productNotificationMediator.Messages.OfType<CatalogProductCreatedEvent>().ToImmutableHashSet();
    CollectionAssert.IsNotEmpty(messages);
    Assert.AreEqual(1, messages.Count);
    Assert.AreEqual(expectedProductGuid, messages.First().Sku);
  }

  public class FakeProductIdMediator : IDomainMediator<ProductId>
  {
    public HashSet<IDomainEvent<ProductId>> Messages { get; } = new();

    public Task PublishAsync(IDomainEvent<ProductId> notificationParam, CancellationToken cancellationTokenParam = default)
    {
      Messages.Add(notificationParam);
      return Task.CompletedTask;
    }

    public Task PublishAsync<TNotification>(TNotification notificationParam, CancellationToken cancellationTokenParam = default)
      where TNotification : IDomainEvent<ProductId>
    {
      Messages.Add(notificationParam);
      return Task.CompletedTask;
    }
  }

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
  }
}