namespace Northwind.Domain.Shipment;

using Core.Domain;
using ValueObjects;

public class ShipmentAggRoot : AggregateBase<ShipmentId>, IHaveIdentity<ShipmentId>
{
  //ref: https://www.youtube.com/watch?v=64ngP-aUYPc

  private ShipmentAggRoot()
  {
    Stops = new List<ShipmentStop>();
  }

  public ShipmentAggRoot(ShipmentId shipmentIDParam, IList<ShipmentStop> stopsParam)
  {
    Id = shipmentIDParam;
    Stops = stopsParam;
  }

  public AddressValueObject? CustomerAddress { get; private set; }

  public DateTime ShipmentDate { get; set; }

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

  public void ConfigureDeliveryAddress(AddressValueObject deliveryAddressParam)
  {
    if (CustomerAddress is not null)
    {
      throw new InvalidOperationException("Delivery address is already set.");
    }

    Stops.Add(new ShipmentPickupStop(1, 1));
    Stops.Add(new ShipmentDeliveryStop(2, 2));
    CustomerAddress = deliveryAddressParam;
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