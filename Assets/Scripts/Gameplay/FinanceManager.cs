using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class FinanceManager: MonoBehaviour
    {
        public static FinanceManager Instance { get; private set; }

      
        [SerializeField]
        int balance = 1000;

        [SerializeField]
        int salaries = 0;

        [SerializeField]
        int creditLimit = -1000;

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

        private void Start()
        {
            TimeManager.Instance.OnDayCompleted += HandleOnDayCompleted;
        }

        void HandleOnDayCompleted(int completedDay)
        {

            // Check if is pay day
            if (IsPayDay(completedDay))
            {
                // Get money from the bank account
                Withdraw(ComputeActualSalaryAll(completedDay));
            }
        }

        bool IsPayDay(int day)
        {
            return day % WorkerAgreement.PayDay == WorkerAgreement.PayDay;
        }

        int ComputeActualSalaryAll(int day)
        {
            int ret = 0;
            foreach (WorkerAgreement wa in HRManager.Instance.Agreements)
            {
                ret += wa.GetActualMonthlySalary(day);
            }
            return ret;
        }

        void Withdraw(int amount)
        {
            balance -= amount;
            if (balance < creditLimit)
                GameOver();
                
        }



        void GameOver() 
        {
            Debug.Log("Bankrupt!!!");
        }
    }

}
