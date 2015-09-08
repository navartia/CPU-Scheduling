using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{
    public class RoundRobin : SchedulingAlgorithm
    {
        public RoundRobin(Process[] processArray) : base(processArray)
        {
            isPreemptive = true;
        }

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
            return 
        }

        protected override void SwapLogic()
        {
            readyQueue.Enqueue(currentProcess, currentProcess.remainingTime);
        }

        private int CalculateQuantum()
        {
            int returnVal = 0;

            int count = processArray.Length;
            foreach (Process process in processArray)
            {
                returnVal = returnVal + process.cpuBurst;
            }

            return Convert.ToInt32((returnVal / count) * 0.8);
        }
    }
}
