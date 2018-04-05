using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace XElevator
{
    interface IElevator
    {
        int CurrentFloor { get; }
        Direction GetStatus();
        AddFloorResponse AddFloor();
        ICollection Destinations();
        Direction Run();
    }
}
