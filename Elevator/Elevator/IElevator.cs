using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Elevator
{
    interface IElevator
    {
        int CurrentFloor();
        Direction GetStatus();
        void AddFloor();
        ICollection GetFloors();
    }
}
