using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elevator;
using Moq;
using System.Collections.Generic;

namespace TestElevators
{
    [TestClass]
    public class XElevatorTest
    {
        [TestMethod]
        public void XElevator_Constructor()
        {
            XElevator e = new XElevator(10);

            Assert.AreEqual(0, e.Destinations.Count);
            Assert.AreEqual(Direction.idle, e.Direction);
            Assert.AreEqual(0, e.Location);
            Assert.AreEqual(10, e.ID);
            Assert.AreEqual(3000, e.Velocity);
            Assert.AreEqual(6000, e.LoadTime);
        }
        /*
                internal class FakeElevator : IXElevator
                {
                    public int Location {
                        get { return 5; }
                        set { }
                    }
                    public Direction Direction { get; set; }
                    public int ID { get { return 0; } }
                }

                [TestMethod]
                public void Controller_Create1ElevatorsWith10Floors_ExpectSuccess()
                {
                    List<FakeElevator> elevatorBank = new List<FakeElevator>();
                    elevatorBank.Add(new FakeElevator());

                    Controller controller = new Controller();
                    controller.SetElevatorBank(elevatorBank)
                    controller.NumberOfFloors = 10;

                    controller
                }
        */
        [TestClass]
        public class AddDestination
        {
            [TestMethod]
            [TestCategory("AddDestination")]
            [ExpectedException(typeof(ArgumentOutOfRangeException), "1001: The floor requested is -1, but must be between 0 and 10")]
            public void XElevator_AddNegativeFloor_CheckException()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(-1);
            }

            [TestMethod]
            [TestCategory("AddDestination")]
            [ExpectedException(typeof(ArgumentOutOfRangeException), "1001: The floor requested is -1, but must be between 0 and 10")]
            public void XElevator_AddHighFloor_CheckException()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(11);
            }

            [TestMethod]
            [TestCategory("AddDestination")]
            public void XElevator_AddDestination_DestinationsContainsFloor()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(5);

                Assert.IsTrue(e.Destinations.Contains(5));
            }
        }

        [TestClass]
        public class Move
        {
            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1000: Destinations expected, but none given.")]
            public void XElevator_MoveIdle_CheckException()
            {
                XElevator e = new XElevator(id: 0);
                e.Location = 5;
                e.Direction = Direction.idle;
                e.Move();
            }

            [TestMethod]
            [TestCategory("Move")]
            public void XElevator_AddDestination_ArrivedNoWait()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(5);
                e.Velocity = 2000;
                e.LoadTime = 500;
                e.Location = 5;
                e.Direction = Direction.emptyDown;

                DateTime before = DateTime.Now;
                bool arrived = e.Move();
                DateTime after = DateTime.Now;

                double diff = (after - before).TotalMilliseconds;
                Assert.IsTrue(diff > 500, "Elevator did not load.");
                Assert.IsTrue(diff < 2000, "Elevator moved.");
                Assert.IsTrue(e.Location == 5, "Elevator is at the wrong location.");
                Assert.IsTrue(arrived == true, "Elevator never arrived.");
            }

            [TestMethod]
            [TestCategory("Move")]
            public void XElevator_AddDestination_ArrivedWait()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(3);
                e.Velocity = 2000;
                e.LoadTime = 500;
                e.Location = 5;
                e.Direction = Direction.emptyDown;

                DateTime before = DateTime.Now;
                bool arrived = e.Move();
                DateTime after = DateTime.Now;

                double diff = (after - before).TotalMilliseconds;
                Assert.IsTrue(diff >= 2000, "Elevator did not move.");
                Assert.IsTrue(e.Location == 4, "Elevator is at the wrong location.");
                Assert.IsTrue(arrived == false, "Elevator should not have arrived.");
            }

            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1002: Elevator is idle with a destination.")]
            public void XElevator_MoveIdleWithDestination_GenerateException()
            {
                XElevator e = new XElevator(0);
                e.Location = 5;
                e.Direction = Direction.idle;
                e.AddDestination(10);
                e.Move();
            }

            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1003: Elevator is out of service.")]
            public void XElevator_MoveDisabled_GenerateException()
            {
                XElevator e = new XElevator(0);
                e.Location = 5;
                e.Direction = Direction.disabled;
                e.AddDestination(10);
                e.Move();
            }
        }
    }
}
