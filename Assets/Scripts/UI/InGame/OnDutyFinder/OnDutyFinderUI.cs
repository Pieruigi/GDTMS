using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class OnDutyFinderUI : MonoBehaviour
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
