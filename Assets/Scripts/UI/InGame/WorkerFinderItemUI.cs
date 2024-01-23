using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class WorkerFinderItemUI : MonoBehaviour
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
            // Remove the place holder
            if (skillGroupContent.childCount > 0)
                DestroyImmediate(skillGroupContent.GetChild(0).gameObject);
            foreach (Skill skill in worker.Skills)
            {
                // Create a new ui item
                GameObject sg = Instantiate(skillGroupPrefab, skillGroupContent);

                // Set skill name and value
                sg.GetComponent<SkillUI>().Init(skill);
            }

            // Init button
            if (WorkerManager.Instance.IsOnDuty(worker))
                buttonHire.interactable = false;
        }

        public void Hire()
        {
            WorkerManager.Instance.Hire(worker);
        }
    }

}
