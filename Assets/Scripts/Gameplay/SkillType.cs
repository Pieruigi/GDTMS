using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class SkillType
    {
       
        static List<SkillType> skillTypes = new List<SkillType>();
        public static IList<SkillType> SkillTypeAll
        {
            get { CheckSkillTypes(); return skillTypes.AsReadOnly(); }
        }

        string _name;
        public string Name
        {
            get { return _name; }
        }

        SkillType(string name)
        {
            _name = name;
        }

       
        static void CheckSkillTypes()
        {
            if (skillTypes.Count == 0)
            {
                List<SkillTypeAsset> assets = new List<SkillTypeAsset>(Resources.LoadAll<SkillTypeAsset>(SkillTypeAsset.ResourceFolder));
                foreach (var st in assets)
                    skillTypes.Add(new SkillType(st.name));
            }
            
        }

        public static SkillType GetSkillType(string skillName)
        {
            // Init skill types if needed
            CheckSkillTypes();

            return skillTypes.Find(s => s._name == skillName);
        }



    }

}
