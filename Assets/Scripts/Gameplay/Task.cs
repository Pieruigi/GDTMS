using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Task
    {
        public const int MaxWorkersPerTask = 2;

        [SerializeField]
        string skillName;

        /// <summary>
        /// Considering 1 step completed in 1 day at a task speed of 1
        /// </summary>
        [SerializeField]
        float steps;

        [SerializeField]
        float progress = 0;

        [SerializeField]
        List<Worker> workers;


        public Task(string skillName, float steps)
        {
            this.skillName = skillName;
            this.steps = steps;
        }

        public void Progress()
        {

        }

        public bool Completed()
        {
            return !(progress < steps);
        }

        public void Assign(Worker worker)
        {
            if (workers.Count == 2)
                return;

            workers.Add(worker);
        }

        public void Unassign(Worker worker)
        {
            workers.Remove(worker);
        }
    }

}
