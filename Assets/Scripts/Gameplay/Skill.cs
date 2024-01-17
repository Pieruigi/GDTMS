using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDTMS
{
    [System.Serializable]
    public class Skill
    {
        public const int MaxMark = 99;
        public const int MinMark = 1;

        static List<SkillAsset> skillAssets;


        [SerializeField]
        SkillAsset asset;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        int mark;
        public int Mark
        {
            get { return mark; }
        }

        [SerializeField]
        SkillType type;
        public SkillType Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return asset.name; }
        }

        string shortName;
        public string ShortName
        {
            get { return shortName; }
        }

        public Skill(SkillAsset asset, int mark)
        {
            this.mark = mark;
            this.asset = asset;
            shortName = asset.ShortName;
            type = SkillType.GetSkillType(asset.TypeAsset);
        }

        public static List<SkillAsset> GetSkillAssets()
        {
            if (skillAssets == null)
                skillAssets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder));
            return skillAssets;
        }

        public int Increase(int value)
        {
            int oldMark = mark;
            mark = Mathf.Min(mark + value, MaxMark);
            return mark - oldMark;
        }

        public float GetDailyCost()
        {
            return mark * asset.CostPerPoint;
        }

        public override string ToString()
        {
            return $"[Skill Name:{asset.name}, Mark:{mark}, Type:{type}]";
        }
    }

}
