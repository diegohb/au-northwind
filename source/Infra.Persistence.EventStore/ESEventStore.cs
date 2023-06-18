namespace Infra.Persistence.EventStore;

using System.Text;
using global::EventStore.Client;
using Newtonsoft.Json;
using Northwind.Core.Domain;
using Northwind.Core.Persistence.EventStore;

public class ESEventStore : IEventStore

{
  private readonly EventStoreClient _client;

  public ESEventStore(string connectionStringParam)
  {
    _client = getEventStoreConnection(connectionStringParam);
  }

  public async Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> eventParam)
    where TAggregateId : IIdentityValueObject
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId idParam)
    where TAggregateId : IIdentityValueObject
  {
    var streamName = idParam.IdAsString();
    var readResult = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start, resolveLinkTos: true);

    if (await readResult.ReadState != ReadState.Ok)
    {
      throw new ArgumentOutOfRangeException();
    }

    return await readResult
      .Select(deserialize)
      .ToListAsync();

    static IDomainEvent<> deserialize(ResolvedEvent resolvedEventParam)
    {
      var dataType = TypeMapper.GetMappedType(resolvedEventParam.Event.EventType);
      var jsonData = Encoding.UTF8.GetString(resolvedEventParam.Event.Data.ToArray());
      var jsonSerializerSettings = new JsonSerializerSettings { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor };
      var data = JsonConvert.DeserializeObject
        (jsonData, dataType, jsonSerializerSettings);

      if (data != null)
      {
        var domainEvent = data as DomainEvent ?? throw new InvalidOperationException();
        return domainEvent;
      }

      throw new NullReferenceException("Unexpected null event data.");
    }
  }

  #region Support Methods

  private static EventStoreClient getEventStoreConnection(string connectionStringParam)
  {
    var settings = EventStoreClientSettings.Create(connectionStringParam);
    return new EventStoreClient(settings);
  }

  #endregion
}