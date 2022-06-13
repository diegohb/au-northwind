namespace Northwind.UnitTests;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Domain.Catalog;

public class FakeCategoryDomainMediator : IDomainMediator<CategoryId>
{
  public HashSet<IDomainEvent<CategoryId>> Messages { get; } = new();

  public Task PublishAsync(IDomainEvent<CategoryId> notificationParam, CancellationToken cancellationTokenParam = default)
  {
    Messages.Add(notificationParam);
    return Task.CompletedTask;
  }

  public Task PublishAsync<TNotification>(TNotification notificationParam, CancellationToken cancellationTokenParam = default)
    where TNotification : IDomainEvent<CategoryId>
  {
    Messages.Add(notificationParam);
    return Task.CompletedTask;
  }

  internal void ResetForTesting()
  {
    Messages.Clear();
  }
}