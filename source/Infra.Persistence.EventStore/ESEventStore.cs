namespace Infra.Persistence.EventStore;

using System.Text;
using global::EventStore.Client;
using Newtonsoft.Json;
using Northwind.Core;
using Northwind.Core.Domain;
using Northwind.Core.Persistence.EventStore;

public class ESEventStore : IEventStore

{
  private readonly EventStoreClient _client;

  private readonly JsonSerializerSettings _jsonSerializerSettings =
    new() { ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor };

  public ESEventStore(string connectionStringParam)
  {
    _client = getEventStoreConnection(connectionStringParam);
  }

  public async Task<AppendResult> AppendEventAsync<TAggregateId>(IDomainEvent<TAggregateId> eventParam)
    where TAggregateId : IIdentityValueObject
  {
    var streamName = eventParam.AggregateId.IdAsString();
    var jsonEvent = JsonConvert.SerializeObject(eventParam, _jsonSerializerSettings);
    var payload = Encoding.UTF8.GetBytes(jsonEvent);
    var eventData = new EventData(Uuid.FromGuid(eventParam.EventId), eventParam.GetType().FullName!, payload);
    var result = await _client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(eventParam.AggregateVersion), new[] { eventData });
    return new AppendResult(result.NextExpectedStreamRevision.ToInt64());
  }

  public async Task<IEnumerable<Event<TAggregateId>>> ReadEventsAsync<TAggregateId>(TAggregateId idParam)
    where TAggregateId : IIdentityValueObject
  {
    var streamName = idParam.IdAsString();
    var readResult = _client.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start, resolveLinkTos: true);

    if (await readResult.ReadState == ReadState.StreamNotFound)
    {
      throw new EventStoreAggregateNotFoundException($"A stream by name '{streamName}' was not found.");
    }

    return await readResult
      .Select(deserialize)
      .ToListAsync();

    Event<TAggregateId> deserialize(ResolvedEvent resolvedEventParam)
    {
      var dataType = TypeProvider.GetTypeFromAnyReferencingAssembly(resolvedEventParam.Event.EventType);
      var jsonData = Encoding.UTF8.GetString(resolvedEventParam.Event.Data.ToArray());
      var data = JsonConvert.DeserializeObject(jsonData, dataType, _jsonSerializerSettings);

      if (data != null)
      {
        var domainEvent = data as IDomainEvent<TAggregateId> ?? throw new InvalidOperationException();
        return new Event<TAggregateId>(domainEvent, long.CreateChecked(resolvedEventParam.Event.EventNumber.ToInt64()));
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