namespace Nrothwind.Domain.Shipment;

public class ShipmentAggRoot
{
  private readonly IList<ShipmentStop> _stops;

  public ShipmentAggRoot(IList<ShipmentStop> stopsParam)
  {
    _stops = stopsParam;
  }

  public void Arrive(int stopIDParam)
  {
    var currentStop = _stops.SingleOrDefault(s => s.StopID == stopIDParam);
    if (currentStop == null)
    {
      throw new InvalidOperationException("Stop does not exist.");
    }

    var previousStopsAreNotDeparted = _stops.Any(s => s.Sequence < currentStop.Sequence && s.Status != StopStatusEnum.Departed);
    if (previousStopsAreNotDeparted)
    {
      throw new InvalidOperationException("Previous stops have not departed.");
    }

    currentStop.Arrive();
  }

  public void Deliver(int stopIDParam) { }

  public bool IsComplete() { }

  public void Pickup(int stopIDParam) { }
}