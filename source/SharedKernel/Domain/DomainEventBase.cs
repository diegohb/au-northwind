namespace SharedKernel.Domain;

public abstract class DomainEventBase<TAggregateId> : IDomainEvent<TAggregateId>, IEquatable<DomainEventBase<TAggregateId>>
{
  protected DomainEventBase(TAggregateId aggregateId)
  {
    AggregateId = aggregateId;
    EventId = Guid.NewGuid();
  }

  protected DomainEventBase(TAggregateId aggregateId, long aggregateVersion) : this(aggregateId)
  {
    AggregateVersion = aggregateVersion;
  }

  public TAggregateId AggregateId { get; }

  public long AggregateVersion { get; }

  public Guid EventId { get; }

  public bool Equals(DomainEventBase<TAggregateId>? other)
  {
    return other != null &&
           EventId.Equals(other.EventId);
  }

  public override bool Equals(object? obj)
  {
    return base.Equals(obj as DomainEventBase<TAggregateId>);
  }

  public override int GetHashCode()
  {
    return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
  }

  public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
}