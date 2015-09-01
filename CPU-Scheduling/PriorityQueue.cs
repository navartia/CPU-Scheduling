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
        private ArrayList queueArray;
        private ArrayList priorityArray;

        public PriorityQueue()
        {
            queueArray = new ArrayList();
            priorityArray = new ArrayList();
        }

        public void Enqueue(T input, int priority)
        {
            Queue<T> queue;
            int count = queueArray.Count;

            if (count != 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (priority == Convert.ToInt32(priorityArray[i]))
                    {
                        queue = queueArray[i] as Queue<T>;
                        queue.Enqueue(input);
                        break;
                    }
                    else if (priority < Convert.ToInt32(priorityArray[i]))
                    {
                        queue = new Queue<T>();
                        queue.Enqueue(input);

                        queueArray.Insert(i, queue);
                        priorityArray.Insert(i, priority);
                        break;
                    }
                }
            }
            else
            {
                queue = new Queue<T>();
                queue.Enqueue(input);

                queueArray.Add(queue);
                priorityArray.Add(priority);
            }
        }

        public T Dequeue()
        {
            if (queueArray.Count != 0)
            {
                Queue<T> queue = queueArray[0] as Queue<T>;

                T returnVal = queue.Dequeue();

                if (queue.Count == 0)
                {
                    queueArray.RemoveAt(0);
                    priorityArray.RemoveAt(0);
                }

                return returnVal;
            }
            else
            {
                return default(T);
            }
        }

        public int Length()
        {
            return queueArray.Count;
        }
    }
}
