using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    public class NameCollection
    {
        static NameCollection instance;
        public static NameCollection Instance
        {
            get { if (instance == null) instance = new NameCollection(); return instance; }
        }

        List<string> names = new List<string>();
        List<string> surnames = new List<string>();

        private NameCollection()
        {
            // Init names somehow
        }

        public string GetName(int index)
        {
            return $"n_{index}";
        }

        public string GetSurname(int index)
        {
            return $"s_{index}";
        }

        public int GetRandomNameIndex()
        {
            return Random.Range(100, 199);
        }

        public int GetRandomSurnameIndex()
        {
            return Random.Range(200, 299);
        }
    }
}

