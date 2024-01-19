using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class FinanceManager
    {
        static FinanceManager instance;
        public static FinanceManager Instance
        {
            get { if (instance == null) instance = new FinanceManager(); return instance; }
        }

        [SerializeField]
        int balance = 1000;

        [SerializeField]
        int salaries = 0;

        [SerializeField]
        int creditLimit = -1000;

        FinanceManager() 
        {
            Debug.Log("Finance manager...");
            TimeManager.OnDayCompleted += HandleOnDayCompleted;
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
            foreach (WorkerAgreement wa in WorkerManager.Instance.OnDutyList)
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
