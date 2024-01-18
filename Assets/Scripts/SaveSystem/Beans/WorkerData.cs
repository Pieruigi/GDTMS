using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class WorkerData
    {
        [SerializeField]
        int nameIndex = -1;
        public int NameIndex
        {
            get { return nameIndex; }
        }

        [SerializeField]
        List<SkillData> skills;
        public List<SkillData> Skills
        {
            get { return skills; }
        }

        //[SerializeField]
        //List<int> skills = new List<int>();

        //[SerializeField]
        //List<int> gainedSkills = new List<int>();

        public WorkerData(int nameIndex, List<SkillData> skills)
        {
            this.nameIndex = nameIndex;
            this.skills = skills;
        }
    }

}
