using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class WorkerFinderUI : MonoBehaviour
    {
        [SerializeField]
        GameObject workerPrefab;

        [SerializeField]
        Transform content;

        [SerializeField]
        Button buttonPrev, buttonNext;

        [SerializeField]
        TMP_Text textPageNum;

        [SerializeField]
        int maxWorkerPerPage = 24;

        [SerializeField]
        bool forceOnDutyOnly = false;

        [SerializeField]
        GameObject skillFilterPrefab;

        [SerializeField]
        Transform filterContent;


        Toggle salaryFilter;

        List<Worker> workers = new List<Worker>();

        int currentPage = 0;
        string salaryFilterName = "salary";
        Toggle defaultFilter;

        private void Awake()
        {
            // Init buttons
            buttonPrev.onClick.AddListener(() => { currentPage--; ShowCurrentPage(); });
            buttonNext.onClick.AddListener(() => { currentPage++; ShowCurrentPage(); });

            

            // Deactivate object
            gameObject.SetActive(false);
        }

        

        private void OnEnable()
        {
            Debug.Log("Enable UI");

           

            // Get list of workers
            if (!forceOnDutyOnly)
            {
                workers = new List<Worker>(WorkerFinder.Instance.SearchList);
            }
            else
            {
                workers = new List<Worker>();
                foreach (WorkerAgreement wa in WorkerManager.Instance.Agreements)
                    workers.Add(wa.Worker);
            }

            // Reset current page
            currentPage = 0;

            if (defaultFilter == null)
                InitFilters();
            else
                ResetFilters();


            
            //// Show current page
            //if(workers.Count > 0)
            //    ShowCurrentPage();

        }

        private void OnDisable()
        {
            //if (!WorkerSearchManager.Instance)
            //    return;

            // Release workers
            ClearWorkerUIAll();

            // Release list
            workers.Clear();
            workers = null;
        }

  

        void ShowCurrentPage()
        {
            // Remove the old page
            ClearWorkerUIAll();

            int offset = currentPage * maxWorkerPerPage;
            
            for(int i=0; i<maxWorkerPerPage; i++)
            {
                int index = i + offset;

                if (workers.Count - 1 < index)
                    break;

                // Instantiate a new worker ui element
                GameObject w = Instantiate(workerPrefab, content);
                // Init worker ui
                w.GetComponent<WorkerFinderItemUI>().Init(workers[index]);
            }

            // Update buttons
            buttonNext.interactable = false;
            buttonPrev.interactable = false;
            if (currentPage > 0)
                buttonPrev.interactable = true;
            if (currentPage < (workers.Count-1) / maxWorkerPerPage)
                buttonNext.interactable = true;

            // Update page num text
            textPageNum.text = currentPage.ToString();
        }

        void ClearWorkerUIAll()
        {
            int count = content.childCount;
            for (int i = 0; i < count; i++)
                DestroyImmediate(content.GetChild(0).gameObject);
        }

        void ResetFilters()
        {
            Toggle[] toggles = filterContent.GetComponentsInChildren<Toggle>();
            foreach (var t in toggles)
            {
                t.isOn = false;
            }
            defaultFilter.isOn = true;
           
        }

        #region filters
        void InitFilters()
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
            OnFilterChanged(true, salaryFilterName);
        }

        void OnFilterChanged(bool value, string filterName)
        {
            if (workers == null || workers.Count == 0)
                return;
            Debug.Log($"ApplyFilter {filterName}");
            if (salaryFilterName.Equals(filterName.ToLower()))
            {
                // Apply salary filter
                workers.Sort(new WorkerFilterUtility(false).CompareByPrice);
            }
            else
            {
                // Apply skill filter
            }

            ShowCurrentPage();
        }
        #endregion
    }

}
