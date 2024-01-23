using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDTMS
{
    public class ProjectManager : MonoBehaviour
    {
        public UnityAction<Project> OnProjectAdded;
        public UnityAction<Project> OnProjectQuit;
        public UnityAction<Project> OnProjectCompleted;
        public UnityAction<Project, Task, Worker> OnTaskAssigned;
        public UnityAction<Project, Task, Worker> OnTaskUnassigned;

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
            OnProjectAdded?.Invoke(project);
        }

        public void QuitProject(Project project)
        {
            projects.Remove(project);
            OnProjectQuit?.Invoke(project);
        }
                
        public void Assign(Project project, Task task, Worker worker)
        {
            if (!projects.Contains(project))
                return;
            if (project.TaskIsFullOfWorkers(task))
                return;
            if (task.IsAssignedTo(worker))
                return;
            task.Assign(worker);
            OnTaskAssigned?.Invoke(project, task, worker);
        }

        public void Unassign(Project project, Task task, Worker worker)
        {
            if (!projects.Contains(project))
                return;
            if (!task.IsAssignedTo(worker))
                return;
            task.Unassign(worker);
            OnTaskUnassigned?.Invoke(project, task, worker);
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
