using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class WorkerFilterUtility
    {

        bool decreasing = false;

        public WorkerFilterUtility(bool decreasing)
        {
            this.decreasing = decreasing;
        }

        public int CompareByPrice(Worker w1, Worker w2)
        {
            if (w1.GetDailyCost() > w2.GetDailyCost())
                return decreasing ? -1 : 1;
            else if (w1.GetDailyCost() < w2.GetDailyCost())
                return decreasing ? 1 : -1;
            else
                return 0;
        }

        
    }

}
