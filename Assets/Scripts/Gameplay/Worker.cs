using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTMS
{
    public class Worker
    {
        public static List<Worker> workers = new List<Worker>();

       
       

        int nameIndex = -1;
        int surnameIndex = -1;

        List<Skill> skills = new List<Skill>();


       
        static Worker CreateWorker(int nameIndex, int surnameIndex, int mark)
        {
            // Instantiate a new worker
            Worker worker = new Worker();

            // Set fields
            worker.nameIndex = nameIndex;
            worker.surnameIndex = surnameIndex;

            // Worker total mark
            Debug.Log($"Worker total mark:{mark}");

            //// Init skills
            //// Get assets 
            //List<SkillAsset> skillAssets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder));
            //List<SkillTypeAsset> groupAssets = new List<SkillTypeAsset>(Resources.LoadAll<SkillTypeAsset>(SkillTypeAsset.ResourceFolder));
            //Debug.Log($"{groupAssets.Count} skills groups loaded.");
            //Debug.Log($"{skillAssets.Count} skills loaded.");
            //// Set minimum for each group
            //int left = worker.seniority.Mark;
            //int[] marks = new int[groupAssets.Count];
            //for(int i=0; i<groupAssets.Count; i++)
            //{
            //    marks[i] = skillAssets.Count(s => s.TypeAsset == groupAssets[i]);
            //    left -= marks[i];
            //}
            
            //// Set preferred mark
            //// Set a preferred skill type
            //int prefIndex = Random.Range(0, groupAssets.Count);
            //int prefMark = Mathf.RoundToInt(left * .5f);
            //prefMark += Random.Range(0, Mathf.RoundToInt(prefMark * .5f));
            //marks[prefIndex] += prefMark;
            //left -= prefMark;
            //// Keep adding points
            //while(left>0)
            //{
            //    int index = Random.Range(0, marks.Length);
            //    int points = Random.Range(1, left + 1);
            //    marks[index] += points;
            //    left -= points;
            //}

            //Debug.Log($"Marks - {worker.seniority.Mark} - [{marks[0]},{marks[1]},{marks[2]}]");
            //for (int i = 0; i < groupAssets.Count; i++)
            //    worker.skills.AddRange(Skill.CreateSkillGroup(groupAssets[i], marks[i]));

            //// Add worker to the list
            //workers.Add(worker);
            return worker;
        }

        static Worker CreateWorker(int mark)
        {
            return CreateWorker(NameCollection.Instance.GetRandomNameIndex(), NameCollection.Instance.GetRandomSurnameIndex(), mark);
        }

        

        /// <summary>
        /// Create a whole bunch of workers
        /// </summary>
        public static void CreateWorkers(int numOfWorkers)
        {
            
            int leftWorkers = numOfWorkers;

            int maxPoints = Skill.MaxMark * Skill.GetSkillAssets().Count + 1; // +1 to adjust for Random.Range() 
            int[] steps = new int[] { Skill.MaxMark, Mathf.RoundToInt(maxPoints * .33f), Mathf.RoundToInt(maxPoints * .66f), maxPoints };

            int[] marks = new int[numOfWorkers];
            for(int i=0; i<numOfWorkers; i++)
            {
                int mark = Mathf.RoundToInt(Random.Range(steps[0], steps[1]));
                if (i > Mathf.RoundToInt(numOfWorkers * .8f)) // High mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[2], steps[3]));
                }
                else if(i > Mathf.RoundToInt(numOfWorkers * .5f)) // Mid mark
                {
                    mark = Mathf.RoundToInt(Random.Range(steps[1], steps[2]));
                }

                CreateWorker(mark);
            }

          
        }

        public static void DebugAll()
        {
            Debug.Log($"[Workers - All:{workers.Count}]");
            foreach (Worker w in workers)
                Debug.Log(w);
        }

        public override string ToString()
        {
            string ret = $"[Worker - Name:{NameCollection.Instance.GetName(nameIndex)}, Surname:{NameCollection.Instance.GetSurname(surnameIndex)}]";
            foreach (Skill s in skills)
                ret += $"\n{s}";
            return ret;
        }


    }

}
