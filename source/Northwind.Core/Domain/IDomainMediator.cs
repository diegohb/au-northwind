namespace Northwind.Core.Domain;

/// <summary>
///   This is a mediator implementation that will publish domain events internally for consumption by other aggregates,
///   services, and higher logical layer handlers.
/// </summary>
/// <typeparam name="TIdentity">The ID of the aggregate or entity associated to the domain event.</typeparam>
public interface IDomainMediator<TIdentity>
  where TIdentity : IIdentityValueObject
{
  Task PublishAsync(IDomainEvent<TIdentity> notificationParam, CancellationToken cancellationTokenParam = default);

  Task PublishAsync<TNotification>(TNotification notificationParam, CancellationToken cancellationTokenParam = default)
    where TNotification : IDomainEvent<TIdentity>;
}