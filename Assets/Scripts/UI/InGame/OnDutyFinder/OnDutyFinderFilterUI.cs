using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class OnDutyFinderFilterUI : WorkerFinderFilterUI
    {
        [SerializeField]
        Toggle taskToggle;

        [SerializeField]
        Toggle wsToggle;

        bool initialized = false;

        string taskFilterName = "task";
        string wsFilterName = "ws";

        protected override void Reset()
        {
            base.Reset();

            if (!initialized)
                InitFilter();
            else
                ResetFilter();
        }

        void InitFilter()
        {
            if (initialized)
                return;

            initialized = true;
            Debug.Log("InitFilter");
            taskToggle.GetComponent<ToggleFilterUI>().Init(taskFilterName, OnFilterChanged);
            wsToggle.GetComponent<ToggleFilterUI>().Init(wsFilterName, OnFilterChanged);
            ResetFilter();
        }

        void ResetFilter()
        {
            taskToggle.isOn = true;
            wsToggle.isOn = true;
        }

        void OnFilterChanged(bool value, string filterName)
        {
            ReportPaginator();
        }

        

        public override void Apply(ref List<object> items)
        {

            // Apply task filter
            if (!taskToggle.isOn)
            {
                // Save items we don't need to show
                List<object> tmpList = new List<object>();
                foreach (var i in items)
                {
                    if (!ProjectManager.Instance.IsAssignedAnyProject((Worker)i))
                        tmpList.Add(i);
                }

                items.RemoveAll(w => tmpList.Contains(w));
          
            }
               
            // Apply workstation filter
            if (!wsToggle.isOn)
            {
                List<object> tmpList = new List<object>();
                foreach (var i in items)
                {
                    if (!WorkstationManager.Instance.HasAssignedAny((Worker)i))
                        tmpList.Add(i);
                }

                items.RemoveAll(w => tmpList.Contains(w));
                    
            }
          
            base.Apply(ref items);
            
        }
    }

}
