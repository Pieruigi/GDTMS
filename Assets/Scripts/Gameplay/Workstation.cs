using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Workstation
    {
        [SerializeField]
        string _name;

        [SerializeField]
        float multiplier = 1;

        Worker user;
        public Worker User
        {
            get { return user; }
            set { user = value; }
        }


        public Workstation()
        {

        }
    }

}
