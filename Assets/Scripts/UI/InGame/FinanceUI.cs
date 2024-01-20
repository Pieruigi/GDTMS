using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDTMS.UI
{
    public class FinanceUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text textBalance;

        [SerializeField]
        TMP_Text textCost;

        // Start is called before the first frame update
        void Start()
        {
            int day = TimeManager.Instance.CurrentDay;
            if(day % FinanceManager.PayDay == 0)
            {
                //textBalance.text = FinanceManager.Instance.ComputeActualSalaryAll(day + FinanceManager.PayDay).ToString();
            }
            else
            {

            }
            //textBalance.text = FinanceManager.Instance.
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
