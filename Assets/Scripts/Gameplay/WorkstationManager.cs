using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS
{
    public class WorkstationManager : MonoBehaviour
    {
        public UnityAction<Workstation> OnWorkstationAssigned;
        public UnityAction<Workstation> OnWorkstationUnassigned;

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

            UnassignWorkstation(workstation);
            workstation.Assign(worker);

            OnWorkstationAssigned?.Invoke(workstation);
        }

        public void UnassignWorkstation(Workstation workstation)
        {
            if (!workstation.IsAssigned())
                return;

            workstation.Unassign(); // We may want to launch some event
            
            OnWorkstationUnassigned?.Invoke(workstation);
        }

        public bool TryGetAssignedWorkstation(Worker worker, out Workstation workstation)
        {
            workstation = workstations.Find(w => w.User == worker);
            return workstation != null;
            
        }

        public bool IsAssigned(Workstation workstation)
        {
            return workstation.IsAssigned();
        }

        public bool AreAllAssigned()
        {
            foreach(Workstation w in workstations)
            {
                if (!w.IsAssigned())
                    return false;
            }

            return true;
        }
        
        public bool HasAssignedAny(Worker worker)
        {
            foreach(var w in workstations)
            {
                if (w.User == worker)
                    return true;
            }
            return false;
        }
        
    }

}
