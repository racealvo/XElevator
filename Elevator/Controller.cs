using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class Controller : IController
    {
        private List<int> upList;
        private List<int> downList;
        private IElevatorBank elevatorBank;

        public int NumberOfFloors { get; set; }
        public int NumberOfElevators { get; set; }

        public Controller(int floors = 10, int elevators = 1)
        {
            NumberOfFloors = floors;
            NumberOfElevators = elevators;
        }

        public void LaunchUI(string input = "")
        {
            if (input == string.Empty)
            {
                while (input.ToLower() != "q")
                {
                    input = Console.ReadLine();
                    ParseInput(input);
                }
            }


        }

        public void ParseInput(string input)
        {

        }

/*
//        private IXElevator[] Elevators;

        public void SetElevatorBank(ElevatorBank elevators)
        {
            elevatorBank = elevators;
        }

        public Controller(int floors = 10, int elevators = 1)
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
*/
        /*
                public int NumberOfFloors { get; set; }

                public Controller(int numberOfElevators, int numberOfFloors)
                {
                    CreateElevatorBank(numberOfElevators);
                }

                public Controller(List<XElevator> elevatorBank)
                {
                    ElevatorBank = elevatorBank;
                }

                // ElevatorBank factory
                public void CreateElevatorBank(int numberOfElevators)
                {
                    ElevatorBank = new List<XElevator>(numberOfElevators);
                }

                // When multiple elevators, then search for idle elevator, or nearest elevator moving in that direction.
                public int FindAvailableElevator(int floor, Direction direction)
                {
                    return 0;
                }

                // Schedule an elevator
                public int CallElevator(int floor, Direction direction)
                {
                    int elevatorID = FindAvailableElevator(floor, direction);
                    ElevatorBank[elevatorID]

                    return elevatorID;
                }
        */
    }
}
