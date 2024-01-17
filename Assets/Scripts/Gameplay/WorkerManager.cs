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
        List<Worker> workers = new List<Worker>();
        public IList<Worker> Workers
        {
            get { return workers.AsReadOnly(); }
        }

        [SerializeField]
        List<Worker> onDutyWorkers = new List<Worker>();


        WorkerManager()
        {
            TimeManager.OnDayCompleted += HandleOnDayCompleted;
        }

        void HandleOnDayCompleted()
        {
            // Update on duty workers
            foreach(Worker w in onDutyWorkers)
            {
                w.IncreaseDaysOnDuty();
            }

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
            foreach(Worker w in onDutyWorkers)
            {
                ret += w.GetDailyCost() * w.DaysOnDuty;
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

                workers.Add(new Worker(mark));
            }


        }

        public void Hire(Worker worker)
        {
            if (onDutyWorkers.Contains(worker))
                return;
            // Remove the worker from the search list
            //workers.Remove(worker);
            // Add the new worker to the onDuty list
            onDutyWorkers.Add(worker);
            
        }

        public void DebugAll()
        {
            Debug.Log($"[Workers - All:{workers.Count}]");
            foreach (Worker w in workers)
                Debug.Log(w);
        }
    }

}
