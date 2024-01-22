using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class ProjectManager : MonoBehaviour
    {
        public static ProjectManager Instance { get; private set; }

        List<Project> projects = new List<Project>();
        public IList<Project> Projects
        {
            get { return projects.AsReadOnly(); }
        }

        List<Project> completedProjects = new List<Project>();

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
        }

        void HandleOnDayCompleted(int day)
        {
            // Set progress for each project
            foreach(Project p in projects)
            {
                p.Progress();
            }
        }

        public void AddProject(Project project)
        {
            projects.Add(project);
        }

        public void QuitProject(Project project)
        {
            projects.Remove(project);
        }
                
        public void Assign(Project project, Worker worker, Task task)
        {
            if (!projects.Contains(project))
                return;
            if (project.TaskIsFullOfWorkers(task))
                return;
            if (task.IsAssignedTo(worker))
                return;
            task.Assign(worker);
        }

        public bool IsAssignedAnyTask(Project project, Worker worker)
        {
            return project.IsAssignedTo(worker); 
        }

        public bool IsAssignedAnyProject(Worker worker)
        {
            foreach(var project in projects)
            {
                if (IsAssignedAnyTask(project, worker))
                    return true;
            }
            return false;
        }
    }

}
