using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.Scriptables
{
    public class WorkstationAsset : ScriptableObject
    {
        public const string ResourceFolder = "Workstations";

        [SerializeField]
        int price;

        [SerializeField]
        [Range(.75f, 1.5f)]
        float workSpeedMultiplier;

        [SerializeField]
        [Range(1,10)]
        int affidability;
    }

}
