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
  public async Task Setup()
  {
    _productRepo = new EventSourcingRepository<CatalogProduct, ProductId>(_eventStore, _productNotificationMediator);

    var newProductId = ProductId.NewProductId(101);
    var expectedProductGuid = Guid.NewGuid();
    _sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(_sut);
  }

  [Test]
  public void ChangeSkuShouldThrowWhenSameSku()
  {
    var expectedOldGuid = _sut.Sku;
    var expectedNewGuid = expectedOldGuid;

    Assert.Throws<InvalidOperationException>(() => _sut.ChangeSku(expectedNewGuid));
  }

  [Test]
  public async Task ChangeSkuShouldUpdateProductSkuWhenNew()
  {
    var expectedOldGuid = _sut.Sku;
    var expectedNewGuid = Guid.NewGuid();

    _sut.ChangeSku(expectedNewGuid);
    var actualUpdatedSku = _sut.Sku;
    Assert.AreEqual(expectedNewGuid, actualUpdatedSku);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductSkuChangedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNotNull(actualMessage);
    Assert.AreEqual(expectedOldGuid, actualMessage.OldSku);
    Assert.AreEqual(expectedNewGuid, actualMessage.NewSku);
  }

  [Test]
  public void RenameShouldThrowWhenNull()
  {
    Assert.Throws<ArgumentNullException>(() => _sut.Rename(string.Empty));
  }

  [Test]
  public async Task RenameShouldThrowWhenSameName()
  {
    var productName = "Banana";
    _sut.Rename(productName);
    await _productRepo.SaveAsync(_sut);

    Assert.Throws<InvalidOperationException>(() => _sut.Rename(productName));
  }

  [Test]
  public async Task RenameShouldUpdateWhenNew()
  {
    var expectedOldName = _sut.Name;
    var expectedNewName = "Bananas";

    _sut.Rename(expectedNewName);
    var actualNewName = _sut.Name;
    Assert.AreEqual(expectedNewName, actualNewName);
    await _productRepo.SaveAsync(_sut);

    var actualMessages = _productNotificationMediator.Messages.OfType<ProductRenamedEvent>().ToImmutableHashSet();
    var actualMessage = actualMessages.First();

    CollectionAssert.IsNotEmpty(actualMessages);
    Assert.IsNotNull(actualMessage);
    Assert.AreEqual(expectedOldName, actualMessage.OldName);
    Assert.AreEqual(expectedNewName, actualMessage.NewName);
  }

  [Test]
  public async Task SaveAndLoadAggregateShouldReturnAddedAggregate()
  {
    var newProductId = ProductId.NewProductId(102);
    var expectedProductGuid = Guid.NewGuid();
    var sut = new CatalogProduct(newProductId, expectedProductGuid);
    await _productRepo.SaveAsync(sut);
    var result = await _productRepo.GetByIdAsync(newProductId);
    Assert.IsNotNull(result);
    Assert.AreEqual(expectedProductGuid, result!.Sku);
    var messages = _productNotificationMediator.Messages.OfType<CatalogProductCreatedEvent>().ToImmutableHashSet();
    CollectionAssert.IsNotEmpty(messages);
    Assert.AreEqual(expectedProductGuid, messages.Single(msg => msg.AggregateId.Equals(newProductId)).Sku);
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