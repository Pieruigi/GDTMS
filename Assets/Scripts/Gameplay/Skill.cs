using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Skill
    {
        [SerializeField]
        string name;

        [SerializeField]
        int value;

        [SerializeField]
        SkillType type;

        private Skill(string name, SkillType type) 
        {
            this.name = name;
            this.type = type;
        }

        public static Skill Create(SkillAsset asset)
        {
            return new Skill(asset.name, SkillType.Create(asset.TypeAsset));
        }
    }

}
