using GDTMS.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class WorkerFinder: MonoBehaviour
    {
        public const int SearchDay = 60;

        public static WorkerFinder Instance { get; private set; }

      
        /// <summary>
        /// Available workers
        /// </summary>
        //[SerializeField]
        List<Worker> searchList = new List<Worker>();
        public IList<Worker> SearchList
        {
            get { return searchList.AsReadOnly(); }
        }

        int listSize = 48;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            CreateOrUpdateSearchList();
            TimeManager.Instance.OnDayCompleted += HandleOnDayCompleted;
        }

        void HandleOnDayCompleted(int day)
        {
            if (day % SearchDay == 0)
                CreateOrUpdateSearchList();
        } 

        /// <summary>
        /// Create a whole bunch of workers
        /// </summary>
        void CreateOrUpdateSearchList()
        {
            int missing = listSize;

            // If the search list already exists we only need to update it
            if (searchList.Count > 0)
            {
                // We remove all the people already working for us
                searchList.RemoveAll(w => WorkerManager.Instance.IsOnDuty(w));
                // Keep going with the normal update
                float updateRatio = .3f;
                int toUpdate = Mathf.RoundToInt(searchList.Count * updateRatio);
                for (int i = 0; i < toUpdate; i++)
                    searchList.RemoveAt(Random.Range(0, searchList.Count));

                missing -= searchList.Count;
            }
            

            // Fill the search list
            //int skillCount = Skill.GetSkillAssets().Count;
            //int maxPoints = Skill.MaxMark * skillCount + 1; // +1 to adjust for Random.Range() 
            int[] steps = new int[4];
            for (int i = 0; i < 4; i++)
                steps[i] = Skill.MaxMark / 2 * (i + 1);
            

            int[] marks = new int[missing];
            for (int i = 0; i < missing; i++)
            {
                int mark = Mathf.RoundToInt(Random.Range(steps[0], steps[1]));
                if (i > Mathf.RoundToInt(missing * .8f)) // High mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[2], steps[3]));
                }
                else if (i > Mathf.RoundToInt(missing * .5f)) // Mid mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[1], steps[2]));
                }

                searchList.Add(new Worker(mark));
            }


        }


        public void DebugAll()
        {
            Debug.Log($"[Workers - All:{searchList.Count}]");
            foreach (Worker w in searchList)
                Debug.Log(w);
        }
    }

}
