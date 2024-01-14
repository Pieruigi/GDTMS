using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.Scriptables
{
    public class SkillAsset : ScriptableObject
    {
        public const string ResourceFolder = "Skills";

        [SerializeField]
        float pricePerPoint = 1;
        public float PricePerPoint
        {
            get { return pricePerPoint; }
        }

        [SerializeField]
        SkillTypeAsset typeAsset;
        public SkillTypeAsset TypeAsset
        {
            get { return typeAsset; }
        }
    }

}
