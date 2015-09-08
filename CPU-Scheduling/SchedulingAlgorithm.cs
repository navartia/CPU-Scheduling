using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{
    public abstract class SchedulingAlgorithm
    {
        protected Process[] processArray;
        protected PriorityQueue<Process> readyQueue;
        protected Queue finishedQueue;
        protected Queue eventQueue;
        protected Boolean isPreemptive;
        protected int time { get; private set; }
        protected Process currentProcess { get; private set; }

        public SchedulingAlgorithm(Process[] processArray)
        {
            this.processArray = processArray;

            readyQueue = new PriorityQueue<Process>();
            finishedQueue = new Queue();
            eventQueue = new Queue();

            time = 0;
        }

        //Abstract Methods
        protected abstract void CheckForArrival();

        protected abstract Boolean SwappingNow();

        protected abstract void SwapLogic();

        //Public Methods
        public void Run()
        {
            currentProcess = null;
            while (!SchedulingIsDone())
            {
                CheckForArrival();

                if (NothingRuns())
                {
                    if (ThereIsProcessInReadyQueue())
                    {
                        LoadProcessIntoCPU();
                    }
                }

                if (ProcessIsPreemptive())
                {
                    if (SwappingNow())
                        DoPreemptiveSwap();
                }

                if (ThereIsProcessToRun())
                {
                    DoCPURun();
                    time++;
                    
                    CheckForTermination();
                }
                else
                {
                    //Idle CPU Process
                    time++;
                }
            }
        }

        public DataTable GetProcessData()
        {
            DataTable processData = new DataTable();
            processData.Columns.Add("Process Number");
            processData.Columns.Add("Arrival Time");
            processData.Columns.Add("CPU Burst");
            processData.Columns.Add("Waiting Time");
            processData.Columns.Add("Turnaround Time");

            foreach (Process process in finishedQueue)
            {
                processData.Rows.Add(new Object[] { process.name,
                                                    process.arrivalTime,
                                                    process.cpuBurst,
                                                    process.waitingTime,
                                                    process.turnaroundTime });
            }

            return processData;
        }
 
        public DataTable GetEventData()
        {
            DataTable eventData = new DataTable();
            eventData.Columns.Add("Process Number");
            eventData.Columns.Add("Start Time");
            eventData.Columns.Add("End Time");

            foreach (Process process in eventQueue)
            {
                eventData.Rows.Add(new Object[] { process.name,
                                                  process.startTime,
                                                  process.endTime, });
            }

            return eventData;
        }

        //Helper Methods
        private void LoadProcessIntoCPU()
        {
            currentProcess = readyQueue.Dequeue() as Process;
            currentProcess.startTime = time;
            CalculateWaitingTime();
        }

        private void DoCPURun()
        {
            currentProcess.Run();
        }

        private void CheckForTermination()
        {
            if (currentProcess.IsTerminated())
            {
                CalculateTurnaroundTime();
                eventQueue.Enqueue(currentProcess);

                finishedQueue.Enqueue(currentProcess);
                currentProcess = null;
            }
        }

        private void DoPreemptiveSwap()
        {
            CalculateTurnaroundTime();
            eventQueue.Enqueue(currentProcess.Clone());

            SwapLogic();
            currentProcess = readyQueue.Dequeue() as Process;
            currentProcess.startTime = time;
        }

        private void CalculateWaitingTime()
        {
            currentProcess.waitingTime = currentProcess.waitingTime + currentProcess.startTime - currentProcess.endTime;
        }

        private void CalculateTurnaroundTime()
        {
            currentProcess.turnaroundTime = currentProcess.waitingTime + currentProcess.cpuBurst;
            currentProcess.endTime = time;
        }

        //Control Methods
        private Boolean ProcessIsPreemptive()
        {
            return isPreemptive;
        }

        private Boolean ThereIsProcessToRun()
        {
            return currentProcess != null;
        }

        private Boolean SchedulingIsDone()
        {
            return finishedQueue.Count == processArray.Length;
        }

        private Boolean ThereIsProcessInReadyQueue()
        {
            return readyQueue.Length() > 0;
        }

        private Boolean NothingRuns()
        {
            return currentProcess == null;
        }
    }
}