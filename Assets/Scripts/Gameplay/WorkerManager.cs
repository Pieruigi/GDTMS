using GDTMS.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class WorkerManager
    {
        static WorkerManager instance;
        public static WorkerManager Instance
        {
            get { if (instance == null) instance = new WorkerManager(); return instance; }
        }

        /// <summary>
        /// Available workers
        /// </summary>
        [SerializeField]
        List<Worker> searchList = new List<Worker>();
        public IList<Worker> SearchList
        {
            get { return searchList.AsReadOnly(); }
        }

        [SerializeField]
        //List<Worker> onDutyList = new List<Worker>();
        List<WorkerAgreement> onDutyList = new List<WorkerAgreement>();


        WorkerManager()
        {
            TimeManager.OnDayCompleted += HandleOnDayCompleted;
        }

       

        void HandleOnDayCompleted()
        {
           
            // Check if is pay day
            if (TimeManager.Instance.IsPayDay())
            {
                // Get money from the bank account
                FinanceManager.Instance.Withdraw(ComputeSalaryAll());
            }
        }

        int ComputeSalaryAll()
        {
            int ret = 0;
            foreach(WorkerAgreement wa in onDutyList)
            {
                ret += wa.Worker.GetDailyCost();// * w.DaysOnDuty;
            }
            return ret;
        }

        /// <summary>
        /// Create a whole bunch of workers
        /// </summary>
        public void CreateWorkers(int numOfWorkers)
        {

            int leftWorkers = numOfWorkers;

            int skillCount = Skill.GetSkillAssets().Count;
            int maxPoints = Skill.MaxMark * skillCount + 1; // +1 to adjust for Random.Range() 
            int[] steps = new int[4];
            for (int i = 0; i < 4; i++)
                steps[i] = Skill.MaxMark * (i + 1);
            

            int[] marks = new int[numOfWorkers];
            for (int i = 0; i < numOfWorkers; i++)
            {
                int mark = Mathf.RoundToInt(Random.Range(steps[0], steps[1]));
                if (i > Mathf.RoundToInt(numOfWorkers * .8f)) // High mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[2], steps[3]));
                }
                else if (i > Mathf.RoundToInt(numOfWorkers * .5f)) // Mid mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[1], steps[2]));
                }

                searchList.Add(new Worker(mark));
            }


        }

        public void Hire(Worker worker)
        {
            if (onDutyList.Exists(w=>w.Worker == worker))
                return;
            // Remove the worker from the search list
            //workers.Remove(worker);
            // Add the new worker to the onDuty list
            onDutyList.Add(new WorkerAgreement(worker, TimeManager.Instance.ElapsedDays));
            
        }

        public bool IsOnDuty(Worker worker)
        {
            return onDutyList.Exists(wa=>wa.Worker == worker);
        }

        #region save system
        public WorkerManagerData GetSaveData()
        {
            List<WorkerData> all = new List<WorkerData>();
            List<WorkerAgreementData> onDutyIds = new List<WorkerAgreementData>();
            List<int> searchIds = new List<int>();

            // Store all workers from the search list and fill the search id list 
            for (int i = 0; i < searchList.Count; i++)
            {
                all.Add(searchList[i].GetSaveData());
                searchIds.Add(i);
            }
                
            // Fill the on duty list and add the remaining workers on the all list
            for(int i=0; i<onDutyList.Count; i++)
            {
                WorkerAgreement wa = onDutyList[i];
                if (searchList.Contains(wa.Worker))
                {
                    // The worker is also on the search list ( this means it has not been uploaded yet )
                    onDutyIds.Add(new WorkerAgreementData(searchList.IndexOf(wa.Worker), wa.StartingDay));
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


            return new WorkerManagerData(all, searchIds, onDutyIds);

        }

        public static void InitWorkerManager(WorkerManagerData data)
        {
            Debug.Log($"Data search id count:{data.SearchIds.Count}");
            // Initialize the search list
            for (int i = 0; i < data.SearchIds.Count; i++)
            {
                // Add a new worker to the search list
                Instance.searchList.Add(new Worker(data.WorkerAll[data.SearchIds[i]]));
            }
            // Check the on duty list
            for (int i = 0; i < data.OnDutyIds.Count; i++)
            {
                // Check if the worker has already been created ( means is in the search list )
                if (data.SearchIds.Contains(data.OnDutyIds[i].WorkerId))
                {
                    // Worker already exists
                    Instance.onDutyList.Add(new WorkerAgreement(Instance.searchList[data.OnDutyIds[i].WorkerId], data.OnDutyIds[i].StartingDay));
                }
                else
                {
                    // Add a new worker in the on duty list
                    Instance.onDutyList.Add(new WorkerAgreement(new Worker(data.WorkerAll[data.OnDutyIds[i].WorkerId]), data.OnDutyIds[i].StartingDay));
                }
            }
        }

        #endregion
        public void DebugAll()
        {
            Debug.Log($"[Workers - All:{searchList.Count}]");
            foreach (Worker w in searchList)
                Debug.Log(w);
        }
    }

}
