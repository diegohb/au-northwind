namespace Northwind.Domain.Shipment;

using Core.Domain;
using ValueObjects;

public abstract class ShipmentStop : EntityBase<ShipmentStopId>
{
  protected ShipmentStop(int stopIDParam, int sequenceParam)
  {
    Sequence = sequenceParam;
    StopID = stopIDParam;
  }

  public AddressValueObject? Address { get; set; }
  public DateTime? Departed { get; set; }
  public DateTime Scheduled { get; set; }
  public int Sequence { get; set; }
  public StopStatusEnum Status { get; set; }
  public int StopID { get; set; }

  public void Arrive()
  {
    if (Status != StopStatusEnum.InTransit)
    {
      throw new InvalidOperationException("Stop has already arrived.");
    }

    Status = StopStatusEnum.Arrived;
  }

  public void Depart()
  {
    if (Status == StopStatusEnum.Departed)
    {
      throw new InvalidOperationException("Stop has already departed.");
    }

    if (Status == StopStatusEnum.InTransit)
    {
      throw new InvalidOperationException("Stop hasn't arrived yet.");
    }

    Status = StopStatusEnum.Departed;
    Departed = DateTime.UtcNow;
  }
}