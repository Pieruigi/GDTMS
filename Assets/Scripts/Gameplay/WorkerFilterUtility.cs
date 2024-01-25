using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class WorkerFilterUtility
    {

        bool decreasing = false;
        string skillName;

        public WorkerFilterUtility(bool decreasing)
        {
            this.decreasing = decreasing;
        }

        public WorkerFilterUtility(bool decreasing, string skillName)
        {
            this.decreasing = decreasing;
            this.skillName = skillName;
        }

        public int CompareBySalary(Worker w1, Worker w2)
        {
            if (w1.GetDailyCost() > w2.GetDailyCost())
                return decreasing ? -1 : 1;
            else if (w1.GetDailyCost() < w2.GetDailyCost())
                return decreasing ? 1 : -1;
            else
                return 0;
        }

        public int CompareBySkill(Worker w1, Worker w2)
        {
            if (w1.GetSkill(skillName).Mark > w2.GetSkill(skillName).Mark)
                return decreasing ? -1 : 1;
            else if (w1.GetSkill(skillName).Mark < w2.GetSkill(skillName).Mark)
                return decreasing ? 1 : -1;
            else
                return 0;
        }

    }

}
