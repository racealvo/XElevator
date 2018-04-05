using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class ElevatorBank : IElevatorBank
    {
        public int NumberOfFloors { get; set; }
        public int NumberOfElevators { get; set; }
        public void SetElevatorBank(IXElevator[] elevatorBank)
        {
            Elevators = elevatorBank;
        }
        private IXElevator[] Elevators;

        public ElevatorBank(int floors = 10, int elevators = 1)
        {
            NumberOfFloors = floors;
            NumberOfElevators = elevators;

            Elevators = new IXElevator[elevators];
            int id = 0;
            foreach (IXElevator elevator in Elevators)
            {
                elevator.ID = id++;
            }
        }

        /// <summary>
        /// 
        /// Return an available elevator, or null if nothing is available
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public IXElevator Call(int floor, Direction direction)
        {
            // call the first idle elevator
            // find an elevator going in the right direction which has not passed the floor
            // will it be faster to use an elevator which is en route or activate an idle elevator
            IXElevator response = null;

            response = WhoIsIdle();
            if (response != null)
            {
                return response;
            }

            return WhoIsClose(floor, direction);
        }

        private IXElevator WhoIsIdle()
        {
            IXElevator response = null;

            foreach (IXElevator elevator in Elevators)
            {
                if (elevator.Direction == Direction.idle)
                {
                    response = elevator;
                    break;
                }
            }

            return response;
        }

        private IXElevator WhoIsClose(int floor, Direction direction)
        {
            IXElevator response = null;

            foreach (IXElevator elevator in Elevators)
            {
                if (elevator.Direction == direction)
                {
                    // going up
                    if (direction == Direction.up)
                    {
                        response = (elevator.Location < floor) ? elevator : null;
                    }
                    else
                    {
                        response = (elevator.Location < floor) ? elevator : null;
                    }
                }
            }

            return response;
        }
    }
}
