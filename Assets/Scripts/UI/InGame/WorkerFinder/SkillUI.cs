using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDTMS.UI
{
    public class SkillUI : MonoBehaviour
    {

        [SerializeField]
        TMP_Text fieldName;

        [SerializeField]
        TMP_Text fieldValue;

        public void Init(Skill skill)
        {
            fieldName.text = skill.ShortName;
            fieldValue.text = skill.Mark.ToString();
        }
    }

}
