using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS
{
    [System.Serializable]
    public class TimeManager : MonoBehaviour
    {
        public UnityAction<int> OnDayStarted;
        public UnityAction<int> OnDayCompleted;

        public static TimeManager Instance { get; private set; }

        //[SerializeField]
        int currentDay = 1;
        public int CurrentDay
        {
            get { return currentDay; }
        }

        float dayInSec = 1;

        int speedIndex = 1;
        int speedIndexDefault = 1;

        float[] speeds = new float[] { .5f, 1f, 2f, 4f, 16f };

        bool playing = false;
        float elapsed = 0f;
        int pauseOnDay = 0;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                speedIndex = speedIndexDefault;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            
            PauseTime();
        }

        private void Update()
        {
            if (!playing)
                return;

            if(elapsed == 0)
            {
                if (pauseOnDay == currentDay)
                {
                    pauseOnDay = 0;
                    playing = false;
                }
                OnDayStarted?.Invoke(currentDay);
            }
           
            elapsed += Time.deltaTime * speeds[speedIndex];
            if(elapsed > dayInSec)
            {
                OnDayCompleted?.Invoke(currentDay);

                currentDay++;
                elapsed = 0;
            }
        }

      

        public void ResumeTime()
        {
            playing = true;
        }

        public void PauseTime()
        {
            playing = false;
        }
               

        public void IncreaseSpeed()
        {
            if (speedIndex < speeds.Length - 1)
                speedIndex++;
        }

        public void DecreaseSpeed()
        {
            if (speedIndex > 0)
                speedIndex--;
        }

        public void MoveByOneDay()
        {
            playing = true;
            speedIndex = speedIndexDefault;
            pauseOnDay = currentDay + 1;
        }


    }

}
