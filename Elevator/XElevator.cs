using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public class XElevator : Controller, IXElevator
    {
        public List<int> Destinations { get; set; }
        public Direction Direction { get; set; }
        public int Location { get; set; }
        public int ID { get; set; }
        public int Velocity { get; set; }
        public int LoadTime { get; set; }
        public Status Status { get; set; }

        public XElevator(int id)
        {
            Destinations = new List<int>();
            Direction = Direction.idle;
            Location = 0;
            ID = id;
            Velocity = 3 * 1000;
            LoadTime = 6 * 1000;
            Status = new Status(ID, Location, Direction);
        }

        private void Log(Level level, string message)
        {
            Logger.Log(level, string.Format("Elevator ID: {0} \n {1}", ID.ToString(), message));
        }

        private void ValidateDestinationConditions(int floor)
        {
            string exception = string.Empty;

            if (floor > NumberOfFloors || floor < 0)
            {
                exception = string.Format("1001: The floor requested is {0}, but must be between 0 and {1}", floor, NumberOfFloors);
            }
            else if ((Direction == Direction.up || Direction == Direction.emptyUp) && (floor < Location))
            {
                exception = string.Format("1004: Elevator direction is contrary to destination.");
            }
            else if ((Direction == Direction.down || Direction == Direction.emptyDown) && (floor > Location))
            {
                exception = string.Format("1004: Elevator direction is contrary to destination.");
            }

            if (!string.IsNullOrEmpty(exception))
            {
                Log(Level.Error, exception);
                throw new ArgumentOutOfRangeException("floor", exception);
            }
        }

        public int AddDestination(int floor)
        {
            Log(Level.Information, string.Format("AddDestination: Floor {0}", floor));
            int response = -1;

            ValidateDestinationConditions(floor);

            if (!Destinations.Contains(floor))
            {
                Destinations.Add(floor);
                response = floor;
            }

            // Sort the destinations
            if (Direction == Direction.up || Direction == Direction.emptyUp)
            {
                Destinations.Sort();
            }
            else
            {
                Destinations.Sort((a, b) => -1 * a.CompareTo(b));
            }

            return response;
        }

        private void ValidateMoveConditions()
        {
            List<string> exceptions = new List<string>();
            bool continueValidation = true;

            if (Direction == Direction.disabled)
            {
                exceptions.Add("1003: Elevator is out of service.");
                continueValidation = false;
            }

            // The Controller must set the direction before attempting a move
            if ((Destinations.Count > 0) &&
                (Direction == Direction.idle || Direction == Direction.disabled))
            {
                exceptions.Add(string.Format("1002: Elevator is {0} with a destination.", Direction.ToString()));
            }

            if (continueValidation)
            {
                // if the elevator is moving, it had better be going somewhere
                if (Destinations.Count == 0)
                {
                    exceptions.Add("1000: Destinations expected, but none given.");
                }
            }

            if (exceptions.Count > 0)
            {
                string aggregate = string.Empty;
                foreach (string exception in exceptions)
                {
                    aggregate += exception + ((exceptions.Count > 1) ? '\r' : '\0');
                }
                Log(Level.Error, aggregate);
                throw new ArgumentException(aggregate, "Destinations");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Move()
        { 
            bool arrived = false;
            int currentDestination = -1;

            ValidateMoveConditions();

            if (Destinations.Contains(Location))
            {
                Destinations.Remove(Location);

                // Are we switching direction?
                if (Destinations.Count == 0)
                {
                    Thread.Sleep(LoadTime);
                }
                arrived = true;
            }

            if (!arrived)
            {
                if (Direction == Direction.up || Direction == Direction.emptyUp)
                {
                    Location++;
                }
                if (Direction == Direction.down || Direction == Direction.emptyDown)
                {
                    Location--;
                }

                // This call is to simulate the journey of the elevator between floors.
                Thread.Sleep(Velocity);
            }

            //while (!arrived)
            //{
            //    if (Destinations.Contains(Location))
            //    {
            //        Destinations.Remove(Location);

            //        // Are we switching direction?
            //        if (Destinations.Count == 0)
            //        {
            //            Thread.Sleep(LoadTime);
            //        }
            //        arrived = true;
            //    }

            //    if (!arrived)
            //    {
            //        if (Direction == Direction.up || Direction == Direction.emptyUp)
            //        {
            //            Location++;
            //            Destinations.l
            //        }
            //        if (Direction == Direction.down || Direction == Direction.emptyDown)
            //        {
            //            Location--;
            //        }

            //        // This call is to simulate the journey of the elevator between floors.
            //        Thread.Sleep(Velocity);
            //    }

            //    Status.Report(this, )
            //}

            return arrived;
        }
    }
}
