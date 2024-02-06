using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class WorkerFinderFilterUI : PaginatorFilterUI
    {
        [SerializeField]
        GameObject skillFilterPrefab;

        [SerializeField]
        Transform filterContent;

        [SerializeField]
        Toggle directionToggle;

        Toggle salaryFilter;

        List<Worker> workers = new List<Worker>();

        int currentPage = 0;
        string salaryFilterName = "salary";
        string lastFilterName;
        Toggle defaultFilter;


        protected override void Reset()
        {
            if (!defaultFilter)
                InitFilter();
            else
                ResetFilter();
        }


        void ResetFilter()
        {
            // Reset all toggles
            Toggle[] toggles = filterContent.GetComponentsInChildren<Toggle>();
            foreach (var t in toggles)
            {
                t.isOn = false;
            }
            
            // Set default toggle on
            if(defaultFilter)
                defaultFilter.isOn = true;

        }



        void InitFilter()
        {
            // Get the toggle group
            ToggleGroup toggleGroup = filterContent.GetComponent<ToggleGroup>();

            // Init the salary filter
            ToggleFilterUI salaryFilter = filterContent.GetComponentInChildren<ToggleFilterUI>();
            defaultFilter = salaryFilter.GetComponent<Toggle>();
            defaultFilter.isOn = true;
            salaryFilter.Init(salaryFilterName, OnFilterChanged, toggleGroup);

            // Init the sill filters
            List<SkillAsset> assets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder));
            foreach (var asset in assets)
            {
                GameObject sf = Instantiate(skillFilterPrefab, filterContent);
                // Set off
                sf.GetComponent<ToggleFilterUI>().Init(asset.name, OnFilterChanged, toggleGroup);
                sf.GetComponent<Toggle>().isOn = false;
            }

            // Init direction toggle
            directionToggle.onValueChanged.AddListener((v) => { OnFilterChanged(true, lastFilterName); });
            directionToggle.transform.SetAsLastSibling();
                     
            ResetFilter();
        }

        /// <summary>
        /// Called by toggle listener
        /// </summary>
        /// <param name="value"></param>
        /// <param name="filterName"></param>
        void OnFilterChanged(bool value, string filterName)
        {
            //OnChanged?.Invoke(filterName);
            if (!value)
                return;

            lastFilterName = filterName;
            ReportPaginator();
        }

        public override void Apply(ref List<object> items)
        {
            workers = items.Cast<Worker>().ToList();

            if (workers == null || workers.Count == 0)
                return;
           
            if (salaryFilterName.Equals(lastFilterName.ToLower()))
            {
                // Apply salary filter
                workers.Sort(new WorkerFilterUtility(directionToggle.isOn).CompareBySalary);
            }
            else
            {
                // Apply skill filter
                workers.Sort(new WorkerFilterUtility(directionToggle.isOn, lastFilterName).CompareBySkill);
            }
            
            items = workers.Cast<object>().ToList();
        }

       


    }

}
