using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XElevator
{
    public enum Direction
    {
        up,
        down,
        idle,
        disabled
    }

    public enum AddFloorResponse
    {
        OK,
        OutOfRange,     // floor requested is too high or too low for the number of floors serviced.
        BadHeading      // return when the requested floor is too high for the downward elevator, or too low for the upward elevator
    }
}
