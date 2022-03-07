namespace Northwind.Core.Domain;

public interface IDomainMediator<TAggregateId>
  where TAggregateId : IIdentityValueObject
{
  Task PublishAsync(IDomainEvent<TAggregateId> notificationParam, CancellationToken cancellationTokenParam = default);

  Task PublishAsync<TNotification>(TNotification notificationParam, CancellationToken cancellationTokenParam = default)
    where TNotification : IDomainEvent<TAggregateId>;
}