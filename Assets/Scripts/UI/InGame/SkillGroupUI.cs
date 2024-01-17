using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDTMS.UI
{
    public class SkillGroupUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text header;

        [SerializeField]
        GameObject skillPrefab;

        [SerializeField]
        Transform content;

        public void Init(List<Skill> skills)
        {
            header.text = skills[0].Type.Name;
            foreach(Skill skill in skills)
            {
                GameObject s = Instantiate(skillPrefab, content);
                s.GetComponent<SkillUI>().Init(skill);
            }
        }
    }

}
