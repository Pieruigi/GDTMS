using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTMS
{
    
    //public enum WorkerStatus { Available, OnDuty }

    [System.Serializable]
    public class Worker
    {
        

        int nameIndex = -1;
        public string Name
        {
            get { return NameCollection.Instance.GetName(nameIndex); }
        }
        int surnameIndex = -1;


        //WorkerStatus status = WorkerStatus.Available;
        //public WorkerStatus Status
        //{
        //    get { return status; }
        //}
        bool onDuty = false;
        public bool OnDuty
        {
            get { return onDuty; }
        }

        [SerializeField]
        List<Skill> skills = new List<Skill>();
        public IList<Skill> Skills
        {
            get { return skills; }
        }

        /// <summary>
        /// Skills gained while on duty
        /// </summary>
        [SerializeField]
        List<Skill> gainedSkills = new List<Skill>();

        
        /// <summary>
        /// How long has been working for us.
        /// Zero means you have just been employed.
        /// </summary>
        int daysOnDuty = 0; 
        public int DaysOnDuty
        {
            get { return daysOnDuty; }
        }

        public Worker(int mark)
        {
            
            // Set fields
            this.nameIndex = NameCollection.Instance.GetRandomNameIndex();
            this.surnameIndex = NameCollection.Instance.GetRandomSurnameIndex();

            
            Debug.Log($"Worker total mark:{mark}");

            // Init skills
            List<SkillAsset> skillAssets = Skill.GetSkillAssets();
            // Init the skill list
            foreach (var s in skillAssets)
                skills.Add(new Skill(s, 1));

            Debug.Log($"Skill assets count:{skillAssets.Count}");
            //List<SkillTypeAsset> skillTypeAssets = SkillType.GetSkillTypeAssets();
            //Debug.Log($"Skill type assets count:{skillTypeAssets.Count}");
            // We want to choose 1 or at most 2 preferred skills giving them most of the worker mark
            Skill[] prefSkills = new Skill[Random.Range(1, 3)];
            Debug.Log($"Preferred skill count:{prefSkills.Length}");
            prefSkills[0] = skills[Random.Range(0, skills.Count)];

            int countt = 0;
            foreach(var s in skills)
            {
                if(s.Type == prefSkills[0].Type)
                    Debug.Log($"Preferred skill 1 type found:{countt}");
                if (s == prefSkills[0])
                    Debug.Log($"Preferred skill 1 found:{countt}");
                countt++;
            }
            

            if(prefSkills.Length > 1)
            {
                List<Skill> sl = null;
                // Set the second skill
                if (Random.Range(0, 5) < 4)
                {
                    // The same group of the first
                    sl = skills.FindAll(s => s.Type == prefSkills[0].Type && s != prefSkills[0]);
                    Debug.Log($"SL_1.Count:{sl.Count}");
                }
                else
                {
                    // A different group
                    sl = skills.FindAll(s => s != prefSkills[0]);
                    Debug.Log($"SL_2.Count:{sl.Count}");
                }

                prefSkills[1] = sl[Random.Range(0, sl.Count)];
            }


            // We start assigning half of the total point to the preferred skills
            int left = mark;
            for(int i=0; i<prefSkills.Length; i++)
            {
                int count = Mathf.RoundToInt(left * (prefSkills.Length == 1 ? .5f : .25f));
                left -= prefSkills[i].Increase(count);
            }

            // Keep adding remaining points
            List<Skill> others = new List<Skill>();
            for (int i = 0; i < skills.Count; i++)
                if (skills[i].Mark < Skill.MaxMark) others.Add(skills[i]);
            while(left>0)
            {
                Skill s = skills[Random.Range(0, others.Count)];
                s.Increase(1);
                if (s.Mark >= Skill.MaxMark)
                    others.Remove(s);
                left--;
            }

            
        }

        public int GetDailyCost()
        {
            int price = 0;
            foreach (var s in skills)
            {
                price += (int) (s.GetDailyCost() - s.GetDailyCost() % 10); // Some kind of normalization
            }

            return price * 8;
        }


        public void Hire()
        {
            onDuty = true;
            daysOnDuty = 0;
        }

        public void IncreaseDaysOnDuty()
        {
            daysOnDuty++;
            // Manage on duty multiplier here
        }

        public override string ToString()
        {
            int mark = 0;
            string ret = "";
            foreach (Skill s in skills)
            {
                ret += $"\n{s}";
                mark += s.Mark;
            }
            ret = $"[Worker - Name:{NameCollection.Instance.GetName(nameIndex)}, Surname:{NameCollection.Instance.GetSurname(surnameIndex)}, Mark:{mark}, $/H:{GetDailyCost()}, OnDuty:{OnDuty}]" + ret;

            return ret;
        }


    }

}
