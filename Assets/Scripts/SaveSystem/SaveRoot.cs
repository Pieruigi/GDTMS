using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class SaveRoot
    {
        [SerializeField]
        WorkerManagerData workerManagerData;
        public WorkerManagerData WorkerManagerData
        {
            get { return workerManagerData; }
        }
        
        public SaveRoot()
        { 
        }


        public void Fill()
        {
            workerManagerData = WorkerManager.Instance.GetSaveData();
        }

        public void Explode()
        {
            WorkerManager.InitWorkerManager(workerManagerData);
        }
    }

}
