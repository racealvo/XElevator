using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class Status
    {
        //static public DateTime ETA { get; set; }
        public int ElevatorID { get; set; }
        public Direction Direction { get; set; }

        public Status(int id, int location, Direction direction)
        {
            Report(id, location, direction);
        }

        public void Report(int id, int floor, Direction direction)
        {
            Console.WriteLine("Elevator {0} is heading {1} currently on floor {2}", id, direction.ToString(), floor);
        }
    }
}
