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
        public int Price
        {
            get { return price; }
        }

        [SerializeField]
        [Range(.75f, 1.5f)]
        float taskSpeedMultiplier;
        public float TaskSpeedMultiplayer
        {
            get { return taskSpeedMultiplier; }
        }

        [SerializeField]
        [Range(1,10)]
        int affidability;
        public int Affidability
        {
            get { return affidability; }
        }
    }

}
