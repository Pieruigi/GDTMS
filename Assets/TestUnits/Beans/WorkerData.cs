using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class WorkerData
    {
        [SerializeField]
        int nameIndex = -1;
        [SerializeField]
        bool onDuty = false;


        public WorkerData(int nameIndex, bool onDuty)
        {
            this.nameIndex = nameIndex;
            this.onDuty = onDuty;
        }
    }

}
