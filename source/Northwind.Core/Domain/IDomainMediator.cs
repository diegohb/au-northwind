namespace Northwind.Core.Domain;

public interface IDomainMediator<TAggregateId>
{
  Task PublishAsync(IDomainEvent<TAggregateId> notification, CancellationToken cancellationToken = default);

  Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
    where TNotification : IDomainEvent<TAggregateId>;
}