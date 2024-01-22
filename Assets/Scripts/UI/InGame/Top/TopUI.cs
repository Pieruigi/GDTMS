using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDTMS.UI
{
    public class TopUI : MonoBehaviour
    {
        [SerializeField]
        TopItemUI workersUI;

        [SerializeField]
        TopItemUI workstationsUI;

        [SerializeField]
        TopItemUI projectsUI;

        string textFormat = "{0}/{1}";

        // Start is called before the first frame update
        void Start()
        {
            CheckWorkers();
            CheckWorkstations();

            WorkstationManager.Instance.OnWorkstationAssigned += HandleWorkstationChanged;
            WorkstationManager.Instance.OnWorkstationUnassigned += HandleWorkstationChanged;
            WorkerManager.Instance.OnWorkerHired += HandleWorkerHired;
        }

        void HandleWorkstationChanged(Workstation workstation)
        {
            CheckWorkstations();
        }

        void CheckWorkstations()
        {
            int assigned = WorkstationManager.Instance.Workstations.Count(w => w.IsAssigned());
            int total = WorkstationManager.Instance.Workstations.Count;
            workstationsUI.SetTextField(string.Format(textFormat, assigned, total));
            if (assigned < total)
                workstationsUI.ShowExclamationPoint();
            else
                workstationsUI.HideExclamationPoint();
        }

        void HandleWorkerHired(Worker worker)
        {
            CheckWorkers();
        }

        void CheckWorkers()
        {
            int assigned = WorkerManager.Instance.Agreements.Count(wa => ProjectManager.Instance.IsAssignedAnyProject(wa.Worker));
            int total = WorkerManager.Instance.Agreements.Count;
            workersUI.SetTextField(string.Format(textFormat, assigned, total));
            if (assigned < total)
                workersUI.ShowExclamationPoint();
            else
                workersUI.HideExclamationPoint();
        }
                        
    }

}
