using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{
    public abstract class SchedulingAlgorithm
    {
        abstract public void Run();
        abstract public DataTable GetProcessData();
        abstract public DataTable GetEventData();

    }
}
