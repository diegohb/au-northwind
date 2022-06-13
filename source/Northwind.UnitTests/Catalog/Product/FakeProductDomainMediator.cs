namespace Northwind.UnitTests.Catalog.Product;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Domain.Catalog;

public class FakeProductDomainMediator : IDomainMediator<ProductId>
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

  internal void ResetForTesting()
  {
    Messages.Clear();
  }
}