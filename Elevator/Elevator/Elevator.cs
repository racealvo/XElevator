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
        public int Floors { get; set; }
        public int CurrentFloor { get; }

        public Elevator(int floors, int floor = 0)
        {
            Floors = floors;
            CurrentFloor = floor;
        }

        public Direction GetStatus()
        {
            throw new NotImplementedException();
        }

        public void AddFloor()
        {
            throw new NotImplementedException();
        }

        public ICollection GetFloors()
        {
            throw new NotImplementedException();
        }
    }
}
