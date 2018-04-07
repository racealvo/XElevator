using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XElevator
{
    public class Elevator : IElevator
    {
        private bool[] destinations;

        public int FloorCount { get; set; }
        public int CurrentFloor { get; }

        public Elevator(int floors, int floor = 0)
        {
            if ((floors <= 1) || (floors > 200))
            {
                throw new ArgumentOutOfRangeException("A minimum of 2 and maximum of 200 floors required.");
            }
            FloorCount = floors;
            CurrentFloor = floor;
            destinations = new bool[floors];
        }

        public Direction GetStatus()
        {
            throw new NotImplementedException();
        }

        // Add floor to list - only if in range
        // Run elevator if it is idle 
        public AddFloorResponse AddFloor()
        {
            throw new NotImplementedException();
        }

        public ICollection Destinations()
        {
            throw new NotImplementedException();
        }

        // This should launch its own thread.
        public RunResponse Run()
        {
            throw new NotImplementedException();
        }
    }
}
