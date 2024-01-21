using GDTMS.SaveSystem;
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

        public const float SkillMaxMul = 2f;
        public const float SkillMinMul = .5f;
        

        static List<SkillAsset> skillAssets;

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
        int initialMark;
        public int InitialMark
        {
            get { return initialMark; }
        }

        [SerializeField]
        float costPerPoint;
        public float CostPerPoint
        {
            get { return costPerPoint; }
        }

        [SerializeField]
        SkillType type;
        public SkillType Type
        {
            get { return type; }
        }

        public string longName;
        public string LongName
        {
            get { return longName; }
        }

        string shortName;
        public string ShortName
        {
            get { return shortName; }
        }

        public Skill(SkillAsset asset, int mark)
        {
            this.mark = mark;
            initialMark = mark;
            longName = asset.name;
            shortName = asset.ShortName;
            costPerPoint = asset.CostPerPoint;
            type = SkillType.GetSkillType(asset.TypeAsset.name);
        }

        public Skill(SkillData saveData)
        {
            mark = saveData.Mark;
            initialMark = saveData.InitialMark;
            longName = saveData.AssetName;
            // Load skill assets
            SkillAsset asset = GetSkillAssets().Find(s => s.name.ToLower().Equals(longName.ToLower()));
            costPerPoint = asset.CostPerPoint;
            shortName = asset.ShortName;
            type = SkillType.GetSkillType(asset.TypeAsset.name);
        }

        public static List<SkillAsset> GetSkillAssets()
        {
            if (skillAssets == null)
                skillAssets = new List<SkillAsset>(Resources.LoadAll<SkillAsset>(SkillAsset.ResourceFolder));
            return skillAssets;
        }

        public void SetInitialValue(int value)
        {
            mark = Mathf.Min(value, MaxMark);
            initialMark = mark;
        }

        public float GetDailyCost()
        {
            return mark * costPerPoint;
        }

        #region save system
        public SkillData GetSaveData()
        {
            return new SkillData(mark, initialMark, longName);
        }

        #endregion

        public override string ToString()
        {
            return $"[Skill Name:{longName}, Mark:{mark}, Type:{type}]";
        }
    }

}
