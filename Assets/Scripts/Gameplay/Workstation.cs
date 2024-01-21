using GDTMS.Scriptables;
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
        public string Name
        {
            get { return _name; }
        }

        [SerializeField]
        float multiplier = 1;
        public float SpeedMultiplier
        {
            get { return multiplier; }
        }

        [SerializeField]
        int affidability;

        [SerializeField]
        int price;

        Worker user;
        public Worker User
        {
            get { return user; }
            set { user = value; }
        }


        public Workstation(WorkstationAsset asset)
        {
            _name = asset.name;
            multiplier = asset.TaskSpeedMultiplayer;
            affidability = asset.Affidability;
            price = asset.Price;
        }

        public void Assign(Worker worker)
        {
            this.User = worker;
        }

        public void Unassign()
        {
            User = null;
        }

        public bool IsAssigned()
        {
            return user != null;
        }
    }

}
