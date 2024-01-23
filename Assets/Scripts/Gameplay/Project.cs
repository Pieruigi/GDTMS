using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Project
    {
        [SerializeField]
        string name;

        [SerializeField]
        List<Task> tasks;
      
        [SerializeField]
        int compensation;

        public Project(List<Task> tasks, int compensation)
        {
            this.tasks = tasks;
            this.compensation = compensation;
            foreach (Task t in tasks)
                Task.OnProgress += HandleOnProgress;
        }

        void HandleOnProgress(Task task, float progress, bool completed)
        {
            Debug.Log($"Project {name}, task {task.SkillName} progress {progress}, is completed:{completed}");
        }

        public void Progress()
        {
            foreach (Task t in tasks)
                t.Progress(); 
        }

        public bool IsCompleted()
        {
            return !tasks.Exists(t=>!t.Completed());
        }

        public bool TaskIsFullOfWorkers(Task task)
        {
            return task.IsFullOfWorkers();
        }

        public bool IsAssignedTo(Worker worker)
        {
            return tasks.Exists(t => t.IsAssignedTo(worker));
        }

        public bool HasAllTasksAssigned()
        {
            foreach (Task t in tasks)
                if (!t.IsAssigned())
                    return false;

            return true;
        }
    }

}
