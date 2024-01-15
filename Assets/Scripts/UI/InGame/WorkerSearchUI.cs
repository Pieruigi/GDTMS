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

        //List<GameObject> workers = new List<GameObject>();

        int currentPage = 0;

        private void Awake()
        {
            // Init buttons
            buttonPrev.onClick.AddListener(() => { currentPage--; ShowCurrentPage(); });
            buttonNext.onClick.AddListener(() => { currentPage++; ShowCurrentPage(); });
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Debug.Log("Enable UI");
            // Reset current page
            currentPage = 0;
            // Show current page
            ShowCurrentPage();
            
        }

        private void OnDisable()
        {
            // Release workers
            ClearWorkers();

            
        }

        void ShowCurrentPage()
        {
            // Remove the old page
            ClearWorkers();

            int offset = currentPage * maxWorkerPerPage;
            
            for(int i=0; i<maxWorkerPerPage; i++)
            {
                int index = i + offset;

                if (WorkerManager.Instance.Workers.Count < i + offset)
                    break;

                // Instantiate a new worker ui element
                GameObject wui = Instantiate(workerPrefab, content);
            }

            // Update buttons
            buttonNext.interactable = false;
            buttonPrev.interactable = false;
            if (currentPage > 0)
                buttonPrev.interactable = true;
            if (currentPage < (WorkerManager.Instance.Workers.Count-1) / maxWorkerPerPage)
                buttonNext.interactable = true;
        }

        void ClearWorkers()
        {
            int count = content.childCount;
            for (int i = 0; i < count; i++)
                DestroyImmediate(content.GetChild(0).gameObject);
        }
    }

}
