using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class WorkerAgreementData
    {
        [SerializeField]
        int workerId;
        public int WorkerId
        {
            get { return workerId; }
        }

        [SerializeField]
        int startingDay;
        public int StartingDay
        {
            get { return startingDay; }
        }

        public WorkerAgreementData(int workerId, int startingDay)
        {
            this.workerId = workerId;
            this.startingDay = startingDay;
        }
    }

}
