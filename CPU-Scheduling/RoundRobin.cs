using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_Scheduling
{
    public class RoundRobin : SchedulingAlgorithm
    {
        public int quantum { get; private set; }
        private int quantumTick;

        public RoundRobin(Process[] processArray) : base(processArray)
        {
            isPreemptive = true;
            quantumTick = 0;
            CalculateQuantum();
        }

        //Overriden Methods
        protected override void ProcessArrival(Process process)
        {
            readyQueue.Enqueue(process, 1);
        }

        protected override void ProcessRun()
        {
            base.ProcessRun();
            quantumTick++;
        }

        protected override void ProcessTermination()
        {
            base.ProcessTermination();
            quantumTick = 0;
        }

        protected override Boolean SwappingNow()
        {
            return quantumTick == quantum;
        }

        protected override void SwapLogic()
        {
            readyQueue.Enqueue(currentProcess, 1);
            quantumTick = 0;
        }

        //Private Methods
        private void CalculateQuantum()
        {
            double average = 0;
            int count = processArray.Length;

            foreach (Process process in processArray)
            {
                average = average + process.cpuBurst;
            }

            average = average / count;
            average = average * 0.8;
            average = Math.Ceiling(average);
            quantum = Convert.ToInt32(average);
        }
    }
}
