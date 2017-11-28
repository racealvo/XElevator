using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    interface IElevatorBank
    {
        int NumberOfFloors { get; set; }
        int NumberOfElevators { get; set; }
        IXElevator Call(int floor, Direction direction);
    }
}
