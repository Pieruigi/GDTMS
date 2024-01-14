using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Worker
    {
        

        int nameIndex = -1;
        int surnameIndex = -1;

        [SerializeField]
        List<Skill> skills = new List<Skill>();

       
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

            // We first set all skills with at least one point
            int left = mark;
            //int[] marks = new int[skillAssets.Count];
            //for (int i = 0; i < marks.Length; i++)
            //    marks[i] = 1;
            //// Update left points
            //left -= marks.Length;

            // We start assigning half of the total point to the preferred skills
            for(int i=0; i<prefSkills.Length; i++)
            {
                int count = Mathf.RoundToInt(left * (prefSkills.Length == 1 ? .5f : .25f));
                //if (count > Skill.MaxMark-1) count = Skill.MaxMark-1; // Skill has been initialized to 1
                //marks[skillAssets.IndexOf(prefSkills[i])] += count;
                left -= prefSkills[i].Increase(count);
            }

            // Keep adding points
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

        public int GetPricePerHour()
        {
            int price = 0;
            foreach (var s in skills)
            {
                price += (int) s.GetPricePerHour();
            }

            return price;
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
            ret = $"[Worker - Name:{NameCollection.Instance.GetName(nameIndex)}, Surname:{NameCollection.Instance.GetSurname(surnameIndex)}, Mark:{mark}, $/H:{GetPricePerHour()}]" + ret;

            return ret;
        }


    }

}
