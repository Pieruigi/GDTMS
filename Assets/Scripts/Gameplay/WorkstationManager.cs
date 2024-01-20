using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class WorkstationManager : MonoBehaviour
    {
        public static WorkstationManager Instance { get; private set; }

        List<Workstation> workstations = new List<Workstation>();
        public IList<Workstation> Workstations
        {
            get { return workstations.AsReadOnly(); }
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

        public void BuyWorkstation(WorkstationAsset asset)
        {
            FinanceManager.Instance.Withdraw(asset.Price);
            workstations.Add(new Workstation(asset));
            
        }

        public void AssignWorkstation(Workstation workstation, Worker worker)
        {
            if (workstation.User == worker)
                return;
            
            workstation.Unassign(); // We may want to launch some event
            workstation.Assign(worker);
        }
    }

}
