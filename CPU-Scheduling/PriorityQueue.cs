using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_Scheduling
{

    public class PriorityQueue<T>
    {
        ArrayList queueArray;
        int highestPriority;

        public PriorityQueue()
        {
            queueArray = new ArrayList();
        }

        public void Enqueue(T input, int priority)
        {
            for (int i = 0; i < queueArray.Count; i++)
            {

            }
        }
    }
}
