namespace Northwind.Core.Domain;

using System.Reflection;
using Persistence;

public abstract class AggregateBase<TId> : EntityBase<TId>, IEventSourcingAggregate<TId>
  where TId : IIdentityValueObject
{
  public const long NewAggregateVersion = -1;

  private readonly ICollection<IDomainEvent<TId>> _uncommittedEvents = new LinkedList<IDomainEvent<TId>>();
  private long _version = NewAggregateVersion;

  protected AggregateBase() { }

  protected AggregateBase(TId idParam) : base(idParam) { }

  void IEventSourcingAggregate<TId>.ApplyEvent(IDomainEvent<TId> eventParam, long versionParam)
  {
    if (!_uncommittedEvents.Any(x => Equals(x.EventId, eventParam.EventId)))
    {
      //((dynamic)this).when((dynamic)eventParam);
      var whenMethods = GetType().GetRuntimeMethods().Where(method => method.Name.Equals("when")).ToArray();
      var whenMethodForEvent = Type.DefaultBinder.SelectMethod
        (BindingFlags.Instance | BindingFlags.NonPublic, whenMethods, new[] { eventParam.GetType() }, null);

      whenMethodForEvent?.Invoke((dynamic)this, new object?[] { eventParam });
      _version = versionParam;
    }
  }

  void IEventSourcingAggregate<TId>.ClearUncommittedEvents()
  {
    _uncommittedEvents.Clear();
  }

  IEnumerable<IDomainEvent<TId>> IEventSourcingAggregate<TId>.GetUncommittedEvents()
  {
    return _uncommittedEvents.AsEnumerable();
  }

  long IEventSourcingAggregate<TId>.Version => _version;

  protected void raiseEvent<TEvent>(TEvent eventParam)
    where TEvent : DomainEventBase<TId>
  {
    var eventWithAggregate = eventParam.WithAggregate
    (
      Equals(Id, default(TId)) ? eventParam.AggregateId : Id,
      _version);

    ((IEventSourcingAggregate<TId>)this).ApplyEvent(eventWithAggregate, _version + 1);
    _uncommittedEvents.Add(eventWithAggregate);
  }
}