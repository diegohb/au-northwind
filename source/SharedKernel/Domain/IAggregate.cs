namespace SharedKernel.Domain;

public interface IAggregate<TId>
{
  TId Id { get; }
}