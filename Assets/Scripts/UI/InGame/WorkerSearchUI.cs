using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class WorkerSearchUI : MonoBehaviour
    {
        [SerializeField]
        GameObject workerPrefab;

        [SerializeField]
        Transform content;

        [SerializeField]
        Button buttonPrev, buttonNext;

        [SerializeField]
        int maxWorkerPerPage = 24;

        [SerializeField]
        bool forceOnDutyOnly = false;

        List<Worker> workers = new List<Worker>();

        int currentPage = 0;

        private void Awake()
        {
            // Init buttons
            buttonPrev.onClick.AddListener(() => { currentPage--; ShowCurrentPage(); });
            buttonNext.onClick.AddListener(() => { currentPage++; ShowCurrentPage(); });

            gameObject.SetActive(false);
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            Debug.Log("Enable UI");
            // Set worker internal list
            //if (!WorkerSearchManager.Instance)
            //    return;
            if (!forceOnDutyOnly)
            {
                workers = new List<Worker>(WorkerSearchManager.Instance.SearchList);
            }
            else
            {
                workers = new List<Worker>();
                foreach (WorkerAgreement wa in HRManager.Instance.Agreements)
                    workers.Add(wa.Worker);
            }
                
                

            // Reset current page
            currentPage = 0;
            // Show current page
            if(workers.Count > 0)
                ShowCurrentPage();
            
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
                w.GetComponent<WorkerUI>().Init(workers[index]);
            }

            // Update buttons
            buttonNext.interactable = false;
            buttonPrev.interactable = false;
            if (currentPage > 0)
                buttonPrev.interactable = true;
            if (currentPage < (workers.Count-1) / maxWorkerPerPage)
                buttonNext.interactable = true;
        }

        void ClearWorkerUIAll()
        {
            int count = content.childCount;
            for (int i = 0; i < count; i++)
                DestroyImmediate(content.GetChild(0).gameObject);
        }
    }

}
