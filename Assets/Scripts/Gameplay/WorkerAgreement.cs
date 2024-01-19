using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class WorkerAgreement
    {
        public const int PayDay = 20;

      
        [SerializeField]
        Worker worker;
        public Worker Worker
        {
            get { return worker; }
        }

        [SerializeField]
        int startingDay = 0;
        public int StartingDay
        {
            get { return startingDay; }
        }

        public WorkerAgreement(Worker worker, int startingDay)
        {
            this.worker = worker;
            this.startingDay = startingDay;
        }

        public int GetActualMonthlySalary(int day)
        {
            int diff = day - StartingDay;
            return worker.GetDailyCost() * (diff >= PayDay ? PayDay : diff);
        }

       
    }

}
