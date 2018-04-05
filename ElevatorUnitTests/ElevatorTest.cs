using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XElevator;

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
        }
        #endregion
    }
}
