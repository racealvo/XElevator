using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    static public class ExceptionRepository
    {
        private static Dictionary<int, string> dictionary = new Dictionary<int, string> {
            {-1,   "-001: Developer Error - Key cannot be found in exception dictionary." },
            {1000, "1000: Destinations expected, but none given.  The elevator was requested to move, but it has no destination."},
            {1001, "1001: The floor requested is {0}, but must be between 0 and {1}."},
            {1002, "1002: Elevator is idle with a destination. The Elevator.Move method may only be called with a moving direction and destination." },
            {1003, "1003: Elevator is out of service." },
            {1004, "1004: Elevator direction is contrary to destination. The destination is in the wrong direction." }
        };

        public static string GetException(int key)
        {
            string response = dictionary[-1];

            if (dictionary.ContainsKey(key))
            {
                response = dictionary[key];
            }

            return response;
        }
    }
}
