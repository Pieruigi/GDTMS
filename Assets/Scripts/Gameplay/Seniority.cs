using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public enum SeniorityLevel { Junior, Mid, Senior }

    public class Seniority
    {
        public const int SeniorityCount = 3;

        SeniorityLevel level;
        
        int value;

        static Vector2[] ranges = new Vector2[] { new Vector2(34, 55), new Vector2(56, 77), new Vector2(78, 99) };

        Seniority(){}

        public static Seniority Create(SeniorityLevel level)
        {
            Seniority ret = new Seniority();
            ret.level = level;
            ret.value = Random.Range((int)ranges[(int)level].x, (int)ranges[(int)level].y+1);
            return ret;
        }

    }

}
