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
        protected abstract Boolean SwappingNow();

        protected abstract void SwapLogic();

        //Virtual Methods
        protected virtual void ProcessArrival(Process process)
        {
            readyQueue.Enqueue(process, process.arrivalTime);
        }

        protected virtual void ProcessLoad()
        {
            currentProcess = readyQueue.Dequeue() as Process;
            currentProcess.startTime = time;
            CalculateWaitingTime();
        }

        protected virtual void ProcessRun()
        {
            currentProcess.Run();
        }

        protected virtual void ProcessSwap()
        {
            CalculateTurnaroundTime();
            eventQueue.Enqueue(currentProcess.Clone());

            SwapLogic();
            currentProcess = readyQueue.Dequeue() as Process;
            currentProcess.startTime = time;
        }

        protected virtual void ProcessTermination()
        {
            CalculateTurnaroundTime();
            eventQueue.Enqueue(currentProcess);

            finishedQueue.Enqueue(currentProcess);
            currentProcess = null;
        }

        //Public Methods
        public void Run()
        {
            currentProcess = null;
            while (!SchedulingIsDone())
            {
                CheckForArrival();

                if (NothingRuns())
                {
                    if (ProcessIsInReadyQueue())
                    {
                        ProcessLoad();
                    }
                }

                CheckForPreemptiveSwap();

                if (ProcessIsLoaded())
                {
                    ProcessRun();
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
        private void CheckForArrival()
        {
            foreach (Process process in processArray)
            {
                if (process.arrivalTime == time)
                {
                    process.Ready();
                    ProcessArrival(process);
                }
            }
        }

        private void CheckForPreemptiveSwap()
        {
            if (ProcessIsPreemptive())
            {
                if (SwappingNow())
                    ProcessSwap();
            }
        }

        private void CheckForTermination()
        {
            if (currentProcess.IsTerminated())
            {
                ProcessTermination();
            }
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
        private Boolean ProcessIsInReadyQueue()
        {
            return readyQueue.Length() > 0;
        }

        private Boolean ProcessIsLoaded()
        {
            return currentProcess != null;
        }

        private Boolean ProcessIsPreemptive()
        {
            return isPreemptive;
        }

        private Boolean SchedulingIsDone()
        {
            return finishedQueue.Count == processArray.Length;
        }

        private Boolean NothingRuns()
        {
            return currentProcess == null;
        }
    }
}