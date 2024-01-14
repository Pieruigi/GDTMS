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

        static List<SkillType> skillTypes = new List<SkillType>();

        
        [SerializeField]
        SkillTypeAsset asset;

        SkillType(SkillTypeAsset asset)
        {
            this.asset = asset;
        }

        public static List<SkillTypeAsset> GetSkillTypeAssets()
        {
            if (skillTypeAssets == null)
                skillTypeAssets = new List<SkillTypeAsset>(Resources.LoadAll<SkillTypeAsset>(SkillTypeAsset.ResourceFolder));
            return skillTypeAssets;
        }

        public static SkillType GetSkillType(SkillTypeAsset asset)
        {
            if (!skillTypes.Exists(s => s.asset == asset))
                skillTypes.Add(new SkillType(asset));

            return skillTypes.Find(s => s.asset == asset);
        }

        

        
        
    }

}
