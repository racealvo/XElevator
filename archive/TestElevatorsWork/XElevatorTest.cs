﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elevator;
using Moq;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

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
            [ExpectedException(typeof(ArgumentOutOfRangeException), "1001: The floor requested is 11, but must be between 0 and 10")]
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

            [TestMethod]
            [TestCategory("AddDestination")]
            public void XElevator_AddDestinationCurrentFloor_FloorNotAddedToDestinations()
            {
                XElevator e = new XElevator(id: 0);
                e.Location = 5;
                e.AddDestination(5);

                Assert.IsTrue(e.Destinations.Count == 1);
                Assert.IsTrue(e.Destinations.Contains(5));
            }

            private XElevator ElevatorFactory(Direction direction, int location)
            {
                XElevator e = new XElevator(id: 0);
                e.Location = location;
                e.Direction = direction;
                e.AddDestination(5);
                e.AddDestination(2);
                e.AddDestination(7);
                e.AddDestination(1);

                return e;
            }

            [TestMethod]
            [TestCategory("AddDestination")]
            public void XElevator_AddUnorderedDestinationsAndElevatorUp_DestinationsListAscending()
            {
                XElevator e = ElevatorFactory(Direction.up, 0);
                int[] expectedDestinations = new int[] { 1, 2, 5, 7 };
                int index = 0;

                foreach (int destination in e.Destinations)
                {
                    Assert.AreEqual(expectedDestinations[index], destination);
                    index++;
                }

                e = ElevatorFactory(Direction.emptyUp, 0);
                index = 0;

                foreach (int destination in e.Destinations)
                {
                    Assert.AreEqual(expectedDestinations[index], destination);
                    index++;
                }
            }

            [TestMethod]
            [TestCategory("AddDestination")]
            public void XElevator_AddUnorderedDestinationsAndElevatorDown_DestinationsListDescending()
            {
                XElevator e = ElevatorFactory(Direction.down, 10);
                int[] expectedDestinations = new int[] { 7, 5, 2, 1 };
                int index = 0;

                foreach (int destination in e.Destinations)
                {
                    Assert.AreEqual(expectedDestinations[index], destination);
                    index++;
                }

                e = ElevatorFactory(Direction.emptyDown, 10);
                index = 0;

                foreach (int destination in e.Destinations)
                {
                    Assert.AreEqual(expectedDestinations[index], destination);
                    index++;
                }
            }
        }


        [TestClass]
        public class Move
        {
            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1000: Destinations expected, but none given.")]
            public async Task XElevator_MoveIdle_CheckException()
            {
                XElevator e = new XElevator(id: 0);
                e.Location = 5;
                e.Direction = Direction.idle;
                Task<bool> moveElevator = e.Move();
                bool arrived = await moveElevator;
            }

            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1002: Elevator is idle with a destination.")]
            public async Task XElevator_MoveIdleWithDestination_GenerateException()
            {
                XElevator e = new XElevator(0);
                e.Location = 5;
                e.Direction = Direction.idle;
                e.AddDestination(10);
                Task<bool> moveElevator = e.Move();
                bool arrived = await moveElevator;
            }

            [TestMethod]
            [TestCategory("Move")]
            [ExpectedException(typeof(ArgumentException), "1003: Elevator is out of service.")]
            public async Task XElevator_MoveDisabled_GenerateException()
            {
                XElevator e = new XElevator(0);
                e.Location = 5;
                e.Direction = Direction.disabled;
                e.AddDestination(10);
                Task<bool> moveElevator = e.Move();
                bool arrived = await moveElevator;
            }

            [TestMethod]
            [TestCategory("Move")]
            // This method also tests logging.
            public async Task XElevator_AddDestination_ArrivedNoWait()
            {
                //Initialize the dependency resolver
                DependencyResolver.Initialize();

                //resolve the type:XElevator
                var e = DependencyResolver.For<IXElevator>();

                //XElevator e = new XElevator(id: 0);
                e.AddDestination(5);
                e.Velocity = 1000;
                e.LoadTime = 10;
                e.Location = 5;
                e.Direction = Direction.emptyDown;

                DateTime before = DateTime.Now;
                Task<bool> moveElevator = e.Move();
                bool arrived = await moveElevator;
                DateTime after = DateTime.Now;

                double diff = (after - before).TotalMilliseconds;
                Assert.IsTrue(diff > 10, "Elevator did not load.");
                Assert.IsTrue(diff < 1000, "Elevator moved.");
                Assert.IsTrue(e.Location == 5, string.Format("Elevator is at the wrong location. Expected 5.  Actual: {0}", e.Location));
                Assert.IsTrue(arrived == true, "Elevator never arrived.");
            }

            [TestMethod]
            [TestCategory("Move")]
            public async Task XElevator_AddDestination_ArrivedWait()
            {
                XElevator e = new XElevator(id: 0);
                e.AddDestination(3);
                e.Velocity = 500;
                e.LoadTime = 10;
                e.Location = 5;
                e.Direction = Direction.emptyDown;

                DateTime before = DateTime.Now;
                Task<bool> moveElevator = e.Move();
                bool arrived = await moveElevator;
                DateTime after = DateTime.Now;

                double diff = (after - before).TotalMilliseconds;
                Assert.IsTrue(diff >= 1000, "Elevator did not move.");
                Assert.IsTrue(e.Location == 3, string.Format("Elevator is at the wrong location. Expected 3.  Actual: {0}", e.Location));
                Assert.IsTrue(arrived == true, "Elevator should have arrived.");
            }
        }
    }
}
