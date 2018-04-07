using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XElevator
{
    public enum RunResponseType
    {
        OK
    }

    public class RunResponse
    {
        public RunResponseType Response { get; set; }
        public Direction Direction { get; set; }
    }
}
