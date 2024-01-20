using GDTMS.SaveSystem;
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

        [SerializeField]
        List<Skill> skills = new List<Skill>();
        public IList<Skill> Skills
        {
            get { return skills; }
        }

        public Worker(int mark)
        {
           

            // Set fields
            this.nameIndex = NameCollection.Instance.GetRandomNameIndex();
            this.surnameIndex = NameCollection.Instance.GetRandomSurnameIndex();


            // Init skills
            List<SkillAsset> skillAssets = Skill.GetSkillAssets();
            // Init the skill list
            foreach (var s in skillAssets)
                skills.Add(new Skill(s, 1));

            // We want to choose 1 or at most 2 preferred skills giving them most of the worker mark
            Skill[] prefSkills = new Skill[Random.Range(1, 3)];
            prefSkills[0] = skills[Random.Range(0, skills.Count)];
            

            if(prefSkills.Length > 1)
            {
                List<Skill> sl = null;
                // Set the second skill
                if (Random.Range(0, 5) < 4)
                {
                    // The same group of the first
                    sl = skills.FindAll(s => s.Type == prefSkills[0].Type && s != prefSkills[0]);
                }
                else
                {
                    // A different group
                    sl = skills.FindAll(s => s != prefSkills[0]);
                }

                prefSkills[1] = sl[Random.Range(0, sl.Count)];
            }


            // We start assigning half of the total point to the preferred skills
            int left = mark;
            for(int i=0; i<prefSkills.Length; i++)
            {
                int count = Mathf.RoundToInt(left * (prefSkills.Length == 1 ? .5f : .25f));
                if (prefSkills[i].Mark + count > Skill.MaxMark)
                    count = Skill.MaxMark - prefSkills[i].Mark;
                prefSkills[i].SetInitialValue(prefSkills[i].Mark + count);
                left -= count;
            }

            // Keep adding remaining points
            List<Skill> others = new List<Skill>();
            for (int i = 0; i < skills.Count; i++)
                if (skills[i].Mark < Skill.MaxMark) others.Add(skills[i]);
            while(left>0)
            {
                Skill s = skills[Random.Range(0, others.Count)];
                s.SetInitialValue(s.Mark + 1);
                if (s.Mark >= Skill.MaxMark)
                    others.Remove(s);
                left--;
            }

            
        }

        public Worker(WorkerData data)
        {
            this.nameIndex = data.NameIndex;
           
            // Skills
            foreach(var s in data.Skills)
            {
                skills.Add(new Skill(s));
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



        #region save system
        public WorkerData GetSaveData()
        {
            // Create skill data list
            List<SkillData> sdl = new List<SkillData>();
            foreach (Skill skill in skills)
                sdl.Add(skill.GetSaveData());

            return new WorkerData(nameIndex, sdl);
        }

        #endregion
        public override string ToString()
        {
            int mark = 0;
            string ret = "";
            foreach (Skill s in skills)
            {
                ret += $"\n{s}";
                mark += s.Mark;
            }
            ret = $"[Worker - Name:{NameCollection.Instance.GetName(nameIndex)}, Surname:{NameCollection.Instance.GetSurname(surnameIndex)}, Mark:{mark}, $/H:{GetDailyCost()}]" + ret;

            return ret;
        }


    }

}
