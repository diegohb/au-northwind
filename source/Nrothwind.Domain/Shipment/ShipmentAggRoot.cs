namespace Nrothwind.Domain.Shipment;

using ValueObjects;

public class ShipmentAggRoot
{
  //ref: https://www.youtube.com/watch?v=64ngP-aUYPc

  public ShipmentAggRoot(int shipmentIDParam, IList<ShipmentStop> stopsParam)
  {
    ShipmentID = shipmentIDParam;
    Stops = stopsParam;
  }

  public AddressValueObject? CustomerAddress { get; private set; }

  public DateTime ShipmentDate { get; set; }

  public int ShipmentID { get; }

  public IList<ShipmentStop> Stops { get; }


  public void Arrive(int stopIDParam)
  {
    var currentStop = Stops.SingleOrDefault(s => s.StopID == stopIDParam);
    if (currentStop == null)
    {
      throw new InvalidOperationException("Stop does not exist.");
    }

    var previousStopsAreNotDeparted = Stops.Any(s => s.Sequence < currentStop.Sequence && s.Status != StopStatusEnum.Departed);
    if (previousStopsAreNotDeparted)
    {
      throw new InvalidOperationException("Previous stops have not departed.");
    }

    currentStop.Arrive();
  }

  public void Deliver(int stopIDParam)
  {
    var currentStop = Stops.SingleOrDefault(s => s.StopID == stopIDParam);
    if (currentStop == null)
    {
      throw new InvalidOperationException("Stop does not exist.");
    }

    if (currentStop is ShipmentDeliveryStop == false)
    {
      throw new InvalidOperationException("Stop is not a delivery.");
    }

    currentStop.Depart();
  }

  public bool IsComplete()
  {
    return Stops.All(s => s.Status == StopStatusEnum.Departed);
  }

  public void Pickup(int stopIDParam)
  {
    var currentStop = Stops.SingleOrDefault(s => s.StopID == stopIDParam);
    if (currentStop == null)
    {
      throw new InvalidOperationException("Stop does not exist.");
    }

    if (currentStop is ShipmentPickupStop == false)
    {
      throw new InvalidOperationException("Stop is not a pickup.");
    }

    currentStop.Depart();
  }
}