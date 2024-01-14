using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class Worker
    {
        public static List<Worker> workers = new List<Worker>();

        int nameIndex = -1;
        int surnameIndex = -1;

        SeniorityLevel seniority;

        
        static Worker CreateWorker(int nameIndex, int surnameIndex, SeniorityLevel seniority, SkillTypeAsset preferredSkillType = null)
        {
            // Instantiate a new worker
            Worker worker = new Worker();

            // Set fields
            worker.nameIndex = nameIndex;
            worker.surnameIndex = surnameIndex;
            worker.seniority = seniority;

            workers.Add(worker);
            return worker;
        }

        static Worker CreateWorker(SeniorityLevel seniority, SkillTypeAsset preferredSkillType = null)
        {
            return CreateWorker(NameCollection.Instance.GetRandomNameIndex(), NameCollection.Instance.GetRandomSurnameIndex(), seniority, preferredSkillType);
        }

        

        /// <summary>
        /// Create a whole bunch of workers
        /// </summary>
        public static void CreateWorkers(int numOfWorkers)
        {
            
            int leftWorkers = numOfWorkers;

            float[] ratios = new float[] { .5f, .7f, 1f };
            for (int i = 0; i < Seniority.SeniorityCount; i++)
            {
                int workerCount = Mathf.FloorToInt(leftWorkers * ratios[i]);
                leftWorkers -= workerCount;

                for (int j = 0; j < workerCount; j++)
                    CreateWorker((SeniorityLevel)i);
            }

          
        }

        public static void DebugAll()
        {
            Debug.Log($"[Workers - All:{workers.Count}, Junior:{workers.FindAll(w=>w.seniority==SeniorityLevel.Junior).Count}, " +
                      $"Mid:{workers.FindAll(w => w.seniority == SeniorityLevel.Mid).Count}, " +
                      $"Senior:{workers.FindAll(w => w.seniority == SeniorityLevel.Senior).Count}]");
            foreach (Worker w in workers)
                Debug.Log(w);
        }

        public override string ToString()
        {
            string ret = $"[Worker - Name:{NameCollection.Instance.GetName(nameIndex)}, Surname:{NameCollection.Instance.GetSurname(surnameIndex)}, Seniority:{seniority}]";
            return ret;
        }


    }

}
