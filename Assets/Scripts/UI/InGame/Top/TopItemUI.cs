using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GDTMS.UI
{
    public class TopItemUI : MonoBehaviour
    {
        [SerializeField]
        TMP_Text textField;

        [SerializeField]
        Image exclamationPoint;


        public void SetTextField(string text)
        {
            textField.text = text;
        }

        public void ShowExclamationPoint()
        {
            exclamationPoint.enabled = true;
        }

        public void HideExclamationPoint()
        {
            exclamationPoint.enabled = false;
        }
    }

}
