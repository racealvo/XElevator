using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XElevator;
using XElevator.Responses;

namespace ElevatorUnitTests
{
    [TestClass]
    public class ElevatorTest
    {
        #region Constructor
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Elevator_Constructor_BadFloors0_ExpectFail()
        {
            Elevator elevator = new Elevator(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Elevator_Constructor_BadFloors1_ExpectFail()
        {
            Elevator elevator = new Elevator(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Elevator_Constructor_BadFloors201_ExpectFail()
        {
            Elevator elevator = new Elevator(201);
        }

        [TestMethod]
        public void Elevator_Constructor_GoodFloors200_ExpectSuccess()
        {
            Elevator elevator = new Elevator(200);
            Assert.AreEqual(elevator.FloorCount, 200);
        }
        #endregion

        #region AddFloor
        [TestMethod]
        public void AddFloor_Add_All_Floors_ExpectSuccess()
        {
            Elevator elevator = new Elevator(2);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(0);
            destinations = elevator.Destinations();
            Assert.AreEqual(AddFloorResponseType.OK, response.Type);
            Assert.AreEqual(1, destinations.Count);
            Assert.AreEqual(0, destinations[0]);

            response = elevator.AddFloor(1);
            destinations = elevator.Destinations();
            Assert.AreEqual(AddFloorResponseType.OK, response.Type);
            Assert.AreEqual(2, elevator.Destinations().Count);
            Assert.AreEqual(0, destinations[0]);
            Assert.AreEqual(1, destinations[1]);
        }
        #endregion
    }
}
