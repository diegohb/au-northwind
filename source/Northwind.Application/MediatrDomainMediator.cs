namespace Northwind.Application;

using Core.Domain;
using MediatR;

public class MediatrDomainMediator<TIdentity> : IDomainMediator<TIdentity>
  where TIdentity : IIdentityValueObject
{
  private readonly IMediator _mediator;

  public MediatrDomainMediator(IMediator mediatorParam)
  {
    _mediator = mediatorParam;
  }

  public async Task PublishAsync(IDomainEvent<TIdentity> notificationParam, CancellationToken cancellationTokenParam = default)

  {
    await _mediator.Publish(notificationParam, cancellationTokenParam);
  }

  async Task IDomainMediator<TIdentity>.PublishAsync<TNotification>(TNotification notificationParam, CancellationToken cancellationTokenParam)
  {
    await PublishAsync(notificationParam, cancellationTokenParam);
  }
}