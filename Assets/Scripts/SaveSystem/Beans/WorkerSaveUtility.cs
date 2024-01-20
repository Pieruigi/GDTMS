using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    
    public class WorkerSaveUtility
    {
        /// <summary>
        /// Return both search and agreement lists data
        /// </summary>
        /// <returns></returns>
        public static WorkerCollectionData GetSaveData()
        {
            List<WorkerData> all = new List<WorkerData>();
            List<WorkerAgreementData> onDutyIds = new List<WorkerAgreementData>();
            List<int> searchIds = new List<int>();

            // Store all workers from the search list and fill the search id list 
            for (int i = 0; i < WorkerSearchManager.Instance.SearchList.Count; i++)
            {
                all.Add(WorkerSearchManager.Instance.SearchList[i].GetSaveData());
                searchIds.Add(i);
            }

            // Fill the on duty list and add the remaining workers on the all list
            for (int i = 0; i < HRManager.Instance.Agreements.Count; i++)
            {
                WorkerAgreement wa = HRManager.Instance.Agreements[i];
                if (WorkerSearchManager.Instance.SearchList.Contains(wa.Worker))
                {
                    // The worker is also on the search list ( this means it has not been uploaded yet )
                    onDutyIds.Add(new WorkerAgreementData(WorkerSearchManager.Instance.SearchList.IndexOf(wa.Worker), wa.StartingDay));
                }
                else
                {
                    // The worker is not on the search list ( we hired them some time ago )
                    // Create a new worker data
                    WorkerData wd = wa.Worker.GetSaveData();
                    // Add the new worker data to the list
                    all.Add(wd);
                    // Set the id in the on duty list
                    onDutyIds.Add(new WorkerAgreementData(all.Count - 1, wa.StartingDay));
                }
            }


            return new WorkerCollectionData(all, searchIds, onDutyIds);

        }

        /// <summary>
        /// Initialize both search and agreement lists
        /// </summary>
        /// <param name="data"></param>
        public static void Init(WorkerCollectionData data)
        {
            Debug.Log($"Data search id count:{data.SearchIds.Count}");
            // Initialize the search list
            for (int i = 0; i < data.SearchIds.Count; i++)
            {
                // Add a new worker to the search list
                WorkerSearchManager.Instance.SearchList.Add(new Worker(data.WorkerAll[data.SearchIds[i]]));
            }
            // Check the on duty list
            for (int i = 0; i < data.OnDutyIds.Count; i++)
            {
                // Check if the worker has already been created ( means is in the search list )
                if (data.SearchIds.Contains(data.OnDutyIds[i].WorkerId))
                {
                    // Worker already exists
                    HRManager.Instance.Agreements.Add(new WorkerAgreement(WorkerSearchManager.Instance.SearchList[data.OnDutyIds[i].WorkerId], data.OnDutyIds[i].StartingDay));
                }
                else
                {
                    // Add a new worker in the on duty list
                    HRManager.Instance.Agreements.Add(new WorkerAgreement(new Worker(data.WorkerAll[data.OnDutyIds[i].WorkerId]), data.OnDutyIds[i].StartingDay));
                }
            }
        }
    }
}

