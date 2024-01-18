using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS.SaveSystem
{
    [System.Serializable]
    public class SkillData
    {
        [SerializeField]
        int mark;
        public int Mark
        {
            get { return mark; }
        }

        [SerializeField]
        int initialMark;
        public int InitialMark
        {
            get { return initialMark; }
        }

        [SerializeField]
        string assetName;
        public string AssetName
        {
            get { return assetName; }
        }

        public SkillData(int mark, int initialMark, string assetName)
        {
            this.mark = mark;
            this.initialMark = initialMark;
            this.assetName = assetName;
        }
    }

}
