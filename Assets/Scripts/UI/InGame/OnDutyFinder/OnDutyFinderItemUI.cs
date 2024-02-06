using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class OnDutyFinderItemUI : PaginatorItemUI
    {
      
        [SerializeField]
        TMP_Text fieldName;

        [SerializeField]
        GameObject skillPrefab;

        [SerializeField]
        Transform skillGroupContent;

        [SerializeField]
        Button buttonTask;

        [SerializeField]
        Button buttonWorkstation;

        [SerializeField]
        TMP_Text textSalary;


        public override void Init(object item)
        {
            base.Init(item);

            Worker worker = (Worker)Item;

            // Set name
            fieldName.text = worker.Name;

            // Set skills
            // Remove the place holder
            if (skillGroupContent.childCount > 0)
                DestroyImmediate(skillGroupContent.GetChild(0).gameObject);
            foreach (Skill skill in worker.Skills)
            {
                // Create a new ui item
                GameObject sg = Instantiate(skillPrefab, skillGroupContent);

                // Set skill name and value
                sg.GetComponent<SkillUI>().Init(skill);
            }

            textSalary.text = (worker.GetDailyCost() * FinanceManager.PayDay).ToString();

            
        }

    }

}
