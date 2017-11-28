using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public interface IXElevator
    {
        Direction Direction { get; set; }
        int Location { get; set; }
        int ID { get; set; }
        List<int> Destinations { get; set; }
    }
}
