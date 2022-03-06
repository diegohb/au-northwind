namespace SharedKernel.Persistence;

using System.Runtime.Serialization;
using Domain;

public interface IRepository<TAggregate, TAggregateId>
  where TAggregate : IAggregate<TAggregateId>
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

  protected RepositoryException
  (
    SerializationInfo info,
    StreamingContext context) : base(info, context) { }
}