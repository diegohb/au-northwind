namespace Northwind.Core.Persistence;

using System.Runtime.Serialization;
using Domain;

public interface IAggregateRepository<TAggregate, TAggregateId>
  where TAggregate : IHaveIdentity<TAggregateId>
  where TAggregateId : IIdentityValueObject
{
  Task<TAggregate?> GetByIdAsync(TAggregateId id);

  Task SaveAsync(TAggregate aggregate);
}

[Serializable]
public class RepositoryException : Exception
{
  public RepositoryException() { }

  public RepositoryException(string message) : base(message) { }

  public RepositoryException(string message, Exception inner) : base(message, inner) { }

  [Obsolete("Obsolete")]
  protected RepositoryException
  (
    SerializationInfo info,
    StreamingContext context) : base(info, context) { }
}