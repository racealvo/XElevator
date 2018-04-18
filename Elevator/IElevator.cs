using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using XElevator.Responses;

namespace XElevator
{
    interface IElevator
    {
        int CurrentFloor { get; }
        Direction Status { get; }
        AddFloorResponse AddFloor(int floor);
        List<int> Destinations();
//        Task<RunResponse> Run();
    }
}
