using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class SaveRoot
    {
        [SerializeField]
        WorkerCollectionData workerManagerData;
        public WorkerCollectionData WorkerManagerData
        {
            get { return workerManagerData; }
        }
        
        public SaveRoot()
        { 
        }


        public void Fill()
        {
            workerManagerData = WorkerSaveUtility.GetSaveData();
        }

        public void Explode()
        {
            WorkerSaveUtility.Init(workerManagerData);
        }
    }

}
