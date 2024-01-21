using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class ProjectFinder : MonoBehaviour
    {
        public const int UpdateDay = 10;

        public static ProjectFinder Instance { get; private set; }

        List<Project> projects = new List<Project>();


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

        void Start()
        {
            TimeManager.Instance.OnDayCompleted += HandleOnDayCompleted;    
        }

        void HandleOnDayCompleted(int day)
        {
            if (day % UpdateDay == 0)
                UpdateProjectList();
        }

        void UpdateProjectList()
        {

        }



        //public void AssignWorker(Task task, Worker worker)
        //{
        //    if (task.Completed())
        //        return;
        //    task.Assign(worker);
        //}

        //public void UnassignWorker(Task task, Worker worker)
        //{
        //    task.Unassign(worker);
        //}
    }

}
