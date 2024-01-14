using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Skill
    {
        public const int MaxMark = 99;
        public const int MinMark = 1;

        static List<SkillAsset> skillAssets;

        [SerializeField]
        string name;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        int mark;

        [SerializeField]
        SkillType type;

        //[SerializeField]
        //Seniority seniority;


        private Skill(string name, int mark, SkillType type) 
        {
            this.name = name;
            this.type = type;
            this.mark = mark;
        }

        


        public static Skill Create(SkillAsset asset, int mark)
        {
            return new Skill(asset.name, mark, SkillType.Create(asset.TypeAsset));
        }

        public static List<Skill> CreateSkillGroup(SkillTypeAsset groupAsset, int mark)
        {
            List<Skill> ret = new List<Skill>();
            // Load resources
            List<SkillAsset> skillAssets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder)).FindAll(s=>s.TypeAsset == groupAsset);
            int left = mark;
            // Add at least 1 point to each skill
            int[] marks = new int[skillAssets.Count];
            for(int i=0; i<marks.Length; i++)
            {
                marks[i] = 1;
                left--;
            }
            // Keep adding point
            while (left > 0)
            {
                int m = Random.Range(1, left + 1);
                left -= m;
                marks[Random.Range(0, marks.Length)] += m;
            }
            // Create skills
            for(int i=0; i<skillAssets.Count; i++)
            {
                SkillAsset s = skillAssets[i];
                ret.Add(Create(s, marks[i]));
            }
            Debug.Log($"Created {skillAssets.Count} skills");
            return ret;
        }

        public static List<SkillAsset> GetSkillAssets()
        {
            if (skillAssets == null)
                skillAssets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder));
            return skillAssets;
        }

        public override string ToString()
        {
            return $"[Skill Name:{name}, Mark:{mark}, Type:{type}]";
        }
    }

}
