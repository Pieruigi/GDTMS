using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS
{
    [System.Serializable]
    public class TimeManager : MonoBehaviour
    {
        public static UnityAction<int> OnDayCompleted;

        public static TimeManager Instance { get; private set; }

        [SerializeField]
        int elapsedDays = 0;
        public int ElapsedDays
        {
            get { return elapsedDays; }
        }

        float dayInSec = 1;

        int speedIndex = 1;
        int speedIndexDefault = 1;

        float[] speeds = new float[] { .5f, 1f, 2f, 4f, 16f };

        bool playing = false;
        float elapsed = 0f;
        bool pauseOnDayCompleted = false;

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

            elapsed += Time.deltaTime * speeds[speedIndex];
            if(elapsed > dayInSec)
            {
                elapsedDays++;
                elapsed = elapsed % dayInSec;

                if (pauseOnDayCompleted)
                {
                    pauseOnDayCompleted = false;
                    playing = false;
                }

                OnDayCompleted?.Invoke(elapsedDays);

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
            pauseOnDayCompleted = true;
            playing = true;
            speedIndex = speedIndexDefault;
        }


    }

}
