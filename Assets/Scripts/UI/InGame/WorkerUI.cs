using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class WorkerUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text fieldName;

        [SerializeField]
        GameObject skillGroupPrefab; 

        [SerializeField]
        Transform skillGroupContent;

        [SerializeField]
        Button buttonHire;

        Worker worker;

        

        public void Init(Worker worker)
        {
            this.worker = worker;

            // Set name
            fieldName.text = worker.Name;

            // Set skills
            List<SkillType> skillTypes = new List<SkillType>(SkillType.SkillTypeAll);
            foreach(SkillType st in skillTypes)
            {
                // Create a new skill group
                GameObject sg = Instantiate(skillGroupPrefab, skillGroupContent);
                
                sg.GetComponent<SkillGroupUI>().Init(new List<Skill>(worker.Skills.Where(s => s.Type == st)));
            }

            // Init button
            if (HRManager.Instance.IsOnDuty(worker))
                buttonHire.interactable = false;
        }

        public void Hire()
        {
            HRManager.Instance.Hire(worker);
        }
    }

}
