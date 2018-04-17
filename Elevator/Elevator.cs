using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XElevator.Responses;

namespace XElevator
{
    public class Elevator : IElevator
    {
        public int FloorCount { get; set; }
        public int CurrentFloor { get; }
        public Direction Status { get; }
        public int ID { get; }

        private bool[] destinations;
        private bool elevatorRunning = false;
        private int Rate { get; }

        public Elevator(int floors, int id, int floor = 0, Direction status = Direction.idle, int rate = 5000)
        {
            if ((floors <= 1) || (floors > 200))
            {
                throw new ArgumentOutOfRangeException("A minimum of 2 and maximum of 200 floors required.");
            }
            FloorCount = floors;
            CurrentFloor = floor;
            destinations = new bool[floors];
            Status = status;
            ID = id;
            Rate = rate;
        }

        // Add floor to list - only if in range
        // range is 0 based where 0 represents the basement
        // Run elevator if it is idle 
        public AddFloorResponse AddFloor(int floor)
        {
            AddFloorResponse response = new AddFloorResponse();

            // check range
            if ((floor < 0) || (floor >= FloorCount))
            {
                response.Type = AddFloorResponseType.OutOfRange;
                return response;
            }

            // Is the floor in the right direction?
            if ( 
                (((Status == Direction.up)   || (Status == Direction.loadingup))   && (CurrentFloor <= floor)) ||
                (((Status == Direction.down) || (Status == Direction.loadingdown)) && (CurrentFloor >= floor)) ||
                (Status == Direction.idle)
               )
            {
                response.Type = AddFloorResponseType.OK;
                destinations[floor] = true;
                return response;
            }

            response.Type = AddFloorResponseType.BadHeading;
            return response;
        }

        public List<int> Destinations()
        {
            List<int> list = new List<int>();

            for (int floor=0; floor<FloorCount; floor++)
            {
                if (destinations[floor])
                {
                    list.Add(floor);
                }
            }

            return list;
        }

        // This should launch its own thread.
        public async Task<RunResponse> Run()
        {
            RunResponse response = new RunResponse();

            // Is the elevator already running
            if (Status != Direction.idle)
            {
                response.Type = (Status == Direction.disabled) ? RunResponseType.Disabled : RunResponseType.AreadyRunning;
            }
            else
            {
                if (elevatorRunning)
                {
                    response.Direction = Status;
                    response.Type = RunResponseType.AreadyRunning;
                }
                else
                {
                    RunElevator();
                    Console.WriteLine("Elevator Run {0} is running.  This should be first!", ID);
                    // Where is the elevator?
                    // What are the elevator destinations - which direction am I heading

                    // Go!
                }
            }

            return response;
        }

        private Task<Direction> RunElevator()
        {
            Thread.Sleep(10000);
            Console.WriteLine("Elevator RunElevator Task {0} is done running.  This should be second.", ID);
            return Task.FromResult(Direction.idle);


            //elevatorRunning = true;

            //List<int> destination_list = Destinations();
            //while (destination_list.Count > 0)
            //{

            //    destination_list = Destinations();
            //}
            //// have we arrived at destination - then load/unload
            //// have we finished loading and have no destinations - then go idle - notify controller
            //// have we finished loading and have destinations - then go

            //elevatorRunning = false;
            //return Direction.idle;
        }
    }
}
