using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDTMS.UI
{
    public class TimeManagerUI : MonoBehaviour
    {

        [SerializeField]
        TMP_Text fieldCurrentDay;

        private void Awake()
        {
            fieldCurrentDay.text = 1.ToString();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            if(TimeManager.Instance)
                TimeManager.Instance.OnDayStarted += HandleOnDayStarted;
        }

        private void OnDisable()
        {
            if(TimeManager.Instance)
                TimeManager.Instance.OnDayStarted -= HandleOnDayStarted;
        }

        void HandleOnDayStarted(int day)
        {
            fieldCurrentDay.text = day.ToString();
        }

        public void Resume()
        {
            TimeManager.Instance.ResumeTime();
        }

        public void Pause()
        {
            TimeManager.Instance.PauseTime();
        }

        public void MoveByOneDay()
        {
            TimeManager.Instance.MoveByOneDay();
        }
    }

}
