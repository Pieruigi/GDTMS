using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class FinanceManager: MonoBehaviour
    {
        public const int PayDay = 20;

        public static FinanceManager Instance { get; private set; }

      
        [SerializeField]
        int balance = 1000;
        public int Balance
        {
            get { return balance; }
        }

        //[SerializeField]
        int salaries = 0;

        [SerializeField]
        int creditLimit = -1000;

        
        int currentMonthSalaries = 0;

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
            TimeManager.Instance.OnDayStarted += HandleOnDayStarted;
        }

        void HandleOnDayStarted(int dayStarted)
        {
            // Reset the monthly cost the day after the payday
            if (dayStarted % PayDay == 1)
                currentMonthSalaries = 0;
        }


        void HandleOnDayCompleted(int completedDay)
        {
            currentMonthSalaries += GetDailyCostAll();

            // Check if is pay day
            if (IsPayDay(completedDay))
            {
                // Get money from the bank account
                Withdraw(currentMonthSalaries);
            }
            
        }

        bool IsPayDay(int day)
        {
            return day % PayDay == 0;
        }
                

        

        

        int GetDailyCostAll()
        {
            int ret = 0;
            foreach(var wa in HRManager.Instance.Agreements)
            {
                Debug.Log($"GetDailyCost():{wa.Worker.GetDailyCost()}");
                ret += wa.Worker.GetDailyCost();
            }
            return ret;
        }

        void GameOver() 
        {
            Debug.Log("Bankrupt!!!");
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
            if (balance < creditLimit)
                GameOver();

        }

        public bool FoundsAreAvailable(int amount)
        {
            if (balance - amount < creditLimit)
                return false;
            else
                return true;
        }
    }

}
