namespace Nrothwind.Domain.Shipment;

public abstract class ShipmentStop
{
  public int Sequence { get; set; }
  public StopStatusEnum Status { get; set; }
  public int StopID { get; set; }

  public void Arrive()
  {
    if (Status != StopStatusEnum.InTransit)
    {
      throw new InvalidOperationException("Stop has laready arrived.");
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
  }
}