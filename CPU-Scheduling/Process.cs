using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_Scheduling
{
    public class Process
    {
        public String name { get; private set; }
        public int arrivalTime { get; private set; }
        public int cpuBurst { get; private set; }
        public int waitingTime { get; private set; }
        public int turnaroundTime { get; private set; }

        public int startTime { get; set; }
        public int remainingTime { get; private set; }
        public int endTime { get; set; }

        private Boolean isReady;
        private Boolean isRunning;
        private Boolean isTerminated;

        public Process(String name, int arrivalTime, int cpuBurst)
        {
            New(name, arrivalTime, cpuBurst);
            Ready();
        }

        public void New(String name, int arrivalTime, int cpuBurst)
        {
            isReady = false;
            isRunning = false;
            isTerminated = false;

            this.name = name;
            this.arrivalTime = arrivalTime;
            this.cpuBurst = cpuBurst;

            remainingTime = cpuBurst;
            endTime = arrivalTime;
        }

        public void Ready()
        {
            isReady = true;
        }

        public void Run()
        {
            isReady = false;
            isRunning = true;

            ComputeForWaitingTime();
            ComputeForTurnaroundTime();

            remainingTime -= 1;
            
            if (remainingTime <= 0)
            {
                isTerminated = true;
                isRunning = false;
            }
        }

        private void ComputeForWaitingTime() 
        {
            waitingTime += startTime - endTime;
        }

        private void ComputeForTurnaroundTime()
        {
            turnaroundTime = waitingTime + cpuBurst;
        }

        public Boolean IsReady()
        {
            return isReady;
        }

        public Boolean IsRunning()
        {
            return isRunning;
        }

        public Boolean IsTerminated()
        {
            return isTerminated;
        }
    }
}