using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.Scriptables
{
    public class SkillAsset : ScriptableObject
    {
        public const string ResourceFolder = "Skills";

        [SerializeField]
        string shortName;
        public string ShortName
        {
            get { return shortName; }
        }

        [SerializeField]
        float costPerPoint = 1;
        public float CostPerPoint
        {
            get { return costPerPoint; }
        }

        [SerializeField]
        SkillTypeAsset typeAsset;
        public SkillTypeAsset TypeAsset
        {
            get { return typeAsset; }
        }
    }

}
