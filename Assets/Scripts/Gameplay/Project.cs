using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Project
    {
        [SerializeField]
        List<Task> tasks;
      
        [SerializeField]
        int compensation;

        
        public void Progress()
        {
            foreach (Task t in tasks)
                t.Progress(); 
        }

        public bool IsCompleted()
        {
            return !tasks.Exists(t=>!t.Completed());
        }
    }

}
