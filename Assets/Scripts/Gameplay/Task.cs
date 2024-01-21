using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS
{
    [System.Serializable]
    public class Task
    {
        /// <summary>
        /// Param1: task
        /// Param2: current progress
        /// Param3: has completed?
        /// </summary>
        public static UnityAction<Task, float, bool> OnProgress;

        public const int MaxWorkersPerTask = 2;

        [SerializeField]
        string skillName;
        public string SkillName
        {
            get { return skillName; }
        }

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
            float speed = 0f;
            // Compute the average multiplayer
          
            foreach(Worker w in workers)
            {
                // Try get the workstation assigned to the current worker
                Workstation workstation;
                if(WorkstationManager.Instance.TryGetAssignedWorkstation(w, out workstation))
                {
                    Skill skill = w.GetSkill(skillName);
                    speed += skill.GetSpeedMultiplier() + workstation.SpeedMultiplier;
                }
            }
            speed /= workers.Count;

            // Working two devs on the same task doesn't really double the speed
            if (workers.Count > 1)
                speed *= .85f;

            progress = Mathf.Min(progress + 1f * speed, steps);

            OnProgress?.Invoke(this, progress, progress == steps);
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
