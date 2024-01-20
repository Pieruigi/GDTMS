using GDTMS.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class HRManager : MonoBehaviour
    {
        public static HRManager Instance { get; private set; }

        [SerializeField]
        List<WorkerAgreement> agreements = new List<WorkerAgreement>();
        public IList<WorkerAgreement> Agreements
        {
            get { return agreements.AsReadOnly(); }
        }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Hire(Worker worker)
        {
            if (agreements.Exists(w => w.Worker == worker))
                return;
            // Remove the worker from the search list
            //workers.Remove(worker);
            // Add the new worker to the onDuty list
            agreements.Add(new WorkerAgreement(worker, TimeManager.Instance.ElapsedDays));

        }

        public bool IsOnDuty(Worker worker)
        {
            return agreements.Exists(wa => wa.Worker == worker);
        }

        #region save system
      
        #endregion

    }

}
