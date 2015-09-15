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
        public FirstComeFirstServed(Process[] processArray) : base(processArray)
        {
            isPreemptive = false;
        }

        //Overriden Methods
        protected override Boolean SwappingNow()
        {
            //Nothing here FCFS is non-preemptive
            return false;
        }

        protected override void SwapLogic()
        {
            //Nothing here FCFS is non-preemptive
            return;
        }
    }
}