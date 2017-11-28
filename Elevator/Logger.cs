using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    enum Level
    {
        Information,
        Warning,
        Error,
        Fatal
    }

    static class Logger
    {
        static public void Log(Level level, string message)
        {
            //Not Yet Implemented
        }
    }
}
