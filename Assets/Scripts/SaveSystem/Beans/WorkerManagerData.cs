using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class WorkerManagerData
    {
        /// <summary>
        /// The whole list of workers
        /// </summary>
        [SerializeField]
        List<WorkerData> workerAll = new List<WorkerData>();
        public IList<WorkerData> WorkerAll
        {
            get { return workerAll.AsReadOnly(); }
        }

        [SerializeField]
        List<int> searchIds = new List<int>();
        public IList<int> SearchIds
        {
            get { return searchIds.AsReadOnly(); }
        }

        [SerializeField]
        List<WorkerAgreementData> onDutyIds = new List<WorkerAgreementData>();
        public IList<WorkerAgreementData> OnDutyIds
        {
            get { return onDutyIds.AsReadOnly(); }
        }

        public WorkerManagerData(List<WorkerData> all, List<int> searchIds, List<WorkerAgreementData> onDutyIds)
        {
            workerAll = all;
            this.searchIds = searchIds;
            this.onDutyIds = onDutyIds;
        }
    }

}
