using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class SkillType
    {
        static List<SkillTypeAsset> skillTypeAssets;

        [SerializeField]
        string name;


        SkillType(string name)
        {
            this.name = name;
        }

        public static List<SkillTypeAsset> GetSkillTypeAssets()
        {
            if (skillTypeAssets == null)
                skillTypeAssets = new List<SkillTypeAsset>(Resources.LoadAll<SkillTypeAsset>(SkillTypeAsset.ResourceFolder));
            return skillTypeAssets;
        }


        public static SkillType Create(SkillTypeAsset asset)
        {
            return new SkillType(asset.name);
        }


        
    }

}
