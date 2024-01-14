using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.Scriptables
{
    public class SeniorityAsset : ScriptableObject
    {
        public const string ResourceFolder = "Seniorities";

        [SerializeField]
        int minValue = 0;
        public int MinValue
        {
            get { return minValue; }
        }

        [SerializeField]
        int maxValue = 0;
        public int MaxValue
        {
            get { return maxValue; }
        }
    }

}
