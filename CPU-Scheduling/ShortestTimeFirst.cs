using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_Scheduling
{
    public class ShortestTimeFirst : SchedulingAlgorithm
    {
        public ShortestTimeFirst(Process[] processArray) : base(processArray)
        {
            isPreemptive = false;
        }

        //Overriden Methods
        protected override void ProcessArrival(Process process)
        {
            readyQueue.Enqueue(process, process.cpuBurst);
        }

        protected override Boolean SwappingNow()
        {
            //Nothing here STF is non-preemptive
            return false;
        }

        protected override void SwapLogic()
        {
            //Nothing here STF is non-preemptive
            return;
        }
    }
}
