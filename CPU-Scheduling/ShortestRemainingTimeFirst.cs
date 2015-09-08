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
    public class ShortestRemainingTimeFirst : SchedulingAlgorithm
    {
        public ShortestRemainingTimeFirst(Process[] processArray) : base(processArray)
        {
            isPreemptive = true;
        }

        //Overriden Methods
        protected override void CheckForArrival()
        {
            foreach (Process process in processArray)
            {
                if (process.arrivalTime == time)
                {
                    process.Ready();
                    readyQueue.Enqueue(process, process.cpuBurst);
                }
            }
        }

        protected override Boolean SwappingNow()
        {
            Process top = readyQueue.Peek();

            if (top != null && currentProcess != null)
                return top.cpuBurst < currentProcess.remainingTime;
            else
                return false;
        }

        protected override void SwapLogic()
        {
            readyQueue.Enqueue(currentProcess, currentProcess.remainingTime);
        }
    }
}
