using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XElevator.Responses
{
    public enum RunResponseType
    {
        OK,
        AreadyRunning,
        Disabled,
        NoDestinations
    }

    public class RunResponse
    {
        public RunResponseType Type { get; set; }
        public Direction Direction { get; set; }
    }
}
