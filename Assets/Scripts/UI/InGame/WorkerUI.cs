using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GDTMS.UI
{
    public class WorkerUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text fieldName;

        //[SerializeField]
        //TMP_Text fieldStatus;

        //[SerializeField]
        //Color availableColor;

        //[SerializeField]
        //Color unavailableColor;

        //[SerializeField]
        //Color ownedColor;

        [SerializeField]
        GameObject skillGroupPrefab; 

        [SerializeField]
        Transform skillGroupContent;


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
        }

        public void Hire()
        {
            WorkerManager.Instance.Hire(worker);
        }
    }

}
