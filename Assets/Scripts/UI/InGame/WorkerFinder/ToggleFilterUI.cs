using GDTMS.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class ToggleFilterUI : MonoBehaviour
    {
        [SerializeField]
        Image imageOff;

        [SerializeField]
        Image imageOn;

        string filterName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

      

        public void Init(string filterName, UnityAction<bool, string> toggleCallback, ToggleGroup group = null, Sprite imageOn = null, Sprite imageOff = null )
        {
            this.filterName = filterName;
            Toggle toggle = GetComponent<Toggle>();
            if (group)
            {
                toggle.group = group;
                //group.RegisterToggle(toggle);
            }
                
            toggle.onValueChanged.AddListener((v) => { toggleCallback(v, filterName); });

            if (imageOn)
                this.imageOn.sprite = imageOn;
            if (imageOff)
                this.imageOff.sprite = imageOff;

        }

    }

}
