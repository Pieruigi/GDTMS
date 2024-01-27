using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public abstract class PaginatorUI : MonoBehaviour
    {
        [SerializeField]
        GameObject itemPrefab;

        [SerializeField]
        Transform content;

        [SerializeField]
        Button buttonPrev, buttonNext;

        [SerializeField]
        TMP_Text textPageNum;

        [SerializeField]
        int maxItemsPerPage = 24;

        int currentPage = 0;
        List<object> items = new List<object>();

        protected abstract IList<object> GetItemList();

        protected virtual void Awake()
        {
            // Init buttons
            buttonPrev.onClick.AddListener(() => { currentPage--; ShowCurrentPage(); });
            buttonNext.onClick.AddListener(() => { currentPage++; ShowCurrentPage(); });



            // Deactivate object
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            Debug.Log("Enable UI");


            items = new List<object>(GetItemList());
            Debug.Log($"Items.Count:{items.Count}");

            // Reset current page
            currentPage = 0;

           

            // Show current page
            if(items.Count > 0)
                ShowCurrentPage();

        }

        protected virtual void OnDisable()
        {
            // Release workers
            ClearAll();

            // Release list
            items.Clear();
          
        }

        void ShowCurrentPage()
        {
            // Remove the old page
            ClearAll();

            int offset = currentPage * maxItemsPerPage;

            for (int i = 0; i < maxItemsPerPage; i++)
            {
                int index = i + offset;

                if (items.Count - 1 < index)
                    break;

                // Instantiate a new worker ui element
                GameObject w = Instantiate(itemPrefab, content);
                // Init worker ui
                w.GetComponent<PaginatorItemUI>().Init(items[index]);
            }

            // Update buttons
            buttonNext.interactable = false;
            buttonPrev.interactable = false;
            if (currentPage > 0)
                buttonPrev.interactable = true;
            if (currentPage < (items.Count - 1) / maxItemsPerPage)
                buttonNext.interactable = true;

            // Update page num text
            textPageNum.text = (currentPage + 1).ToString();
        }

        void ClearAll()
        {
            int count = content.childCount;
            for (int i = 0; i < count; i++)
                DestroyImmediate(content.GetChild(0).gameObject);
        }
    }

}
