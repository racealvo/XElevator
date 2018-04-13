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
            Elevator elevator = new Elevator(0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Elevator_Constructor_BadFloors1_ExpectFail()
        {
            Elevator elevator = new Elevator(1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Elevator_Constructor_BadFloors201_ExpectFail()
        {
            Elevator elevator = new Elevator(201, 0);
        }

        [TestMethod]
        public void Elevator_Constructor_GoodFloors200_ExpectSuccess()
        {
            Elevator elevator = new Elevator(200, 0);
            Assert.AreEqual(elevator.FloorCount, 200);
        }
        #endregion

        #region AddFloor
        [TestMethod]
        public void AddFloor_Add_All_Floors_ExpectSuccess()
        {
            Elevator elevator = new Elevator(2, 0);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(0);
            Assert.AreEqual(AddFloorResponseType.OK, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(1, destinations.Count);
            Assert.AreEqual(0, destinations[0]);

            response = elevator.AddFloor(1);
            Assert.AreEqual(AddFloorResponseType.OK, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(2, elevator.Destinations().Count);
            Assert.AreEqual(0, destinations[0]);
            Assert.AreEqual(1, destinations[1]);
        }

        [TestMethod]
        public void AddFloor_Floor_Is_Below_Upward_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(3, 0, 1, Direction.up);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(0);
            Assert.AreEqual(AddFloorResponseType.BadHeading, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, elevator.Destinations().Count);
        }

        [TestMethod]
        public void AddFloor_Floor_Is_Above_Downward_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(3, 0, 1, Direction.down);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(2);
            Assert.AreEqual(AddFloorResponseType.BadHeading, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, elevator.Destinations().Count);
        }

        [TestMethod]
        public void AddFloor_Floor_Is_Below_LoadingUp_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(3, 0, 1, Direction.loadingup);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(0);
            Assert.AreEqual(AddFloorResponseType.BadHeading, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, elevator.Destinations().Count);
        }

        [TestMethod]
        public void AddFloor_Floor_Is_Above_LoadingDown_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(3, 0, 1, Direction.loadingdown);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(2);
            Assert.AreEqual(AddFloorResponseType.BadHeading, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, elevator.Destinations().Count);
        }

        [TestMethod]
        public void AddFloor_Elevator_Is_On_Floor_Of_Upward_Elevator_ExpectSuccess()
        {
            Elevator elevator = new Elevator(2, 0, 1, Direction.up);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(1);
            Assert.AreEqual(AddFloorResponseType.OK, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(1, destinations.Count);
            Assert.AreEqual(1, destinations[0]);
        }

        [TestMethod]
        public void AddFloor_Floor_Out_Of_Range_Of_Upward_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(2, 0, 1, Direction.up);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(-1);
            Assert.AreEqual(AddFloorResponseType.OutOfRange, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, destinations.Count);
        }

        [TestMethod]
        public void AddFloor_Floor_Out_Of_Range_Of_LoadingUp_Elevator_ExpectFailure()
        {
            Elevator elevator = new Elevator(2, 0, 1, Direction.loadingup);
            List<int> destinations = null;
            AddFloorResponse response = null;

            response = elevator.AddFloor(10);
            Assert.AreEqual(AddFloorResponseType.OutOfRange, response.Type);
            destinations = elevator.Destinations();
            Assert.AreEqual(0, destinations.Count);
        }
        #endregion

        #region Run
        [TestMethod]
        public void RunIt()
        {
            Elevator elevator = new Elevator(2, 0);

            List<int> destinations = null;
            AddFloorResponse response = null;

            elevator.AddFloor(1);
            elevator.Run();
        }
        #endregion
    }
}
