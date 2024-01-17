using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class FinanceManager
    {
        static FinanceManager instance;
        public static FinanceManager Instance
        {
            get { if (instance == null) instance = new FinanceManager(); return instance; }
        }

        int balance = 1000;
        int cost = 0;
        int creditLimit = -1000;

        FinanceManager() 
        { 
        
        }

        public void Withdraw(int amount)
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
