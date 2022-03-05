namespace Northwind.UnitTests;

using System;
using System.Collections.Generic;
using Nrothwind.Domain.Shipment;
using NUnit.Framework;

[TestFixture]
public class ShipmentTestFixture
{
  private ShipmentAggRoot _shipmentRoot = null!;

  [SetUp]
  public void Setup()
  {
    var stops = new List<ShipmentStop> { new ShipmentPickupStop(1, 1), new ShipmentDeliveryStop(2, 2) };
    _shipmentRoot = new ShipmentAggRoot(1, stops);
  }


  [Test]
  public void ArriveStopDoesNotExist()
  {
    Assert.Throws<InvalidOperationException>(() => _shipmentRoot.Pickup(10), "Stop doesn't exist.");
  }

  [Test]
  public void CannotArriveTwice()
  {
    Assert.Throws<InvalidOperationException>
    (() =>
    {
      _shipmentRoot.Arrive(1);
      _shipmentRoot.Pickup(1);
      _shipmentRoot.Deliver(2);
      _shipmentRoot.Deliver(2);
    }, "Stop has already departed.");
  }

  [Test]
  public void CannotDeliverTwice()
  {
    Assert.Throws<InvalidOperationException>
    (() =>
    {
      _shipmentRoot.Arrive(1);
      _shipmentRoot.Arrive(1);
    }, "Stop has already departed.");
  }

  [Test]
  public void CannotDeliverWithoutArriving()
  {
    Assert.Throws<InvalidOperationException>(() => _shipmentRoot.Deliver(2), "Stop hasn't arrived yet.");
  }

  [Test]
  public void CannotPickupAtDelivery()
  {
    Assert.Throws<InvalidOperationException>(() => _shipmentRoot.Pickup(2), "Stop is not a pickup.");
  }

  [Test]
  public void CannotPickupWithoutArriving()
  {
    Assert.Throws<InvalidOperationException>(() => _shipmentRoot.Pickup(1), "Stop hasn't arrived yet.");
  }

  [Test]
  public void CompleteShipmentShouldBeTrue()
  {
    //act
    _shipmentRoot.Arrive(1);
    _shipmentRoot.Pickup(1);
    _shipmentRoot.Arrive(2);
    _shipmentRoot.Deliver(2);

    //assert
    Assert.IsTrue(_shipmentRoot.IsComplete());
  }
}