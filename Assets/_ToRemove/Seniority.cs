//using GDTMS.Scriptables;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace GDTMS
//{
//    public enum SeniorityLevel { Junior, Mid, Senior }

//    public class Seniority
//    {
//        public const int SeniorityCount = 3;

//        SeniorityLevel level;
//        public SeniorityLevel Level
//        {
//            get { return level; }
//        }
        
//        int mark;
//        public int Mark
//        {
//            get { return mark; }
//        }

//        static Vector2[] markRanges = new Vector2[] { new Vector2(34, 55) * 2, new Vector2(56, 77) * 2, new Vector2(78, 99) * 2 };

//        Seniority(){}

//        public static Seniority Create(SeniorityLevel level)
//        {
//            Seniority ret = new Seniority();
//            ret.level = level;
//            ret.mark = Random.Range((int)markRanges[(int)level].x, (int)markRanges[(int)level].y+1);
//            return ret;
//        }

//        public override string ToString()
//        {
//            return $"[Seniority Name:{Level.ToString()}, Mark:{Mark}]";
//        }
//    }

//}
