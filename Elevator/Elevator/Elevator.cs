using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    class Elevator : IElevator
    {
        
        public int FloorCount { get; set; }
        public int CurrentFloor { get; }

        public Elevator(int floors, int floor = 0)
        {
            FloorCount = floors;
            CurrentFloor = floor;
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

        public ICollection GetFloors()
        {
            throw new NotImplementedException();
        }
    }
}
