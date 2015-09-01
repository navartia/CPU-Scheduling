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
    public class FirstComeFirstServed : SchedulingAlgorithm
    {
        private Process[] processArray;
        private Process currentProcess;

        private Queue readyQueue;
        private Queue finishedQueue;
        private Queue eventQueue;

        private Boolean isRunning;
        private int time; 

        public FirstComeFirstServed(Process[] processArray)
        {
            this.processArray = processArray;

            readyQueue = new Queue();
            finishedQueue = new Queue();
            eventQueue = new Queue();

            isRunning = false;
            time = 0;
        }

        //Public Methods
        public override void Run()
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
                        isRunning = true;
                    }
                }

                if(ThereIsProcessToRun())
                {
                    DoCPURun();
                    time++;

                    CheckForTermination();

                    if (ProcessIsPreemptive())
                    {
                        DoPreemptiveSwap();
                    }
                }
                else
                {
                    //Idle CPU Process
                    time++;
                }
            }
        }

        public override DataTable GetProcessData()
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

        public override DataTable GetEventData()
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
                    readyQueue.Enqueue(process);
                }
            }
        }

        private void LoadProcessIntoCPU()
        {
            currentProcess = readyQueue.Dequeue() as Process;
            currentProcess.startTime = time;
        }

        private void DoCPURun()
        {
            currentProcess.Run();
            Console.WriteLine(currentProcess.name + " " + currentProcess.startTime);
        }

        private void CheckForTermination()
        {
            if (currentProcess.IsTerminated())
            {
                CalculateMetric();
                finishedQueue.Enqueue(currentProcess);
                eventQueue.Enqueue(currentProcess);

                isRunning = false;
            }
        }

        private void DoPreemptiveSwap()
        {
            //Nothing here, FCFS is non-preemptive
        }

        private void CalculateMetric()
        {
            currentProcess.waitingTime = currentProcess.waitingTime + currentProcess.startTime - currentProcess.endTime;
            currentProcess.turnaroundTime = currentProcess.waitingTime + currentProcess.cpuBurst;
            currentProcess.endTime = time;
        }

        //Control Methods
        private Boolean ProcessIsPreemptive()
        {
            return false;
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
            return readyQueue.Count > 0;
        }

        private Boolean NothingRuns()
        {
            return !isRunning;
        }
    }
}