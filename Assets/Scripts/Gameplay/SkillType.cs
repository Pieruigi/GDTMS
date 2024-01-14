using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class SkillType
    {
        [SerializeField]
        string name;


        SkillType(string name)
        {
            this.name = name;
        }

        public static SkillType Create(SkillTypeAsset asset)
        {
            return new SkillType(asset.name);
            
        }
        
    }

}
