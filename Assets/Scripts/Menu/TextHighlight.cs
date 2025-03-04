using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    class TextHighlight : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public TMP_Text buttonText;
        private string originalText;

        void Start()
        {
            if (buttonText == null)
                buttonText = GetComponentInChildren<TextMeshProUGUI>();
        
            if (buttonText != null)
                originalText = buttonText.text;
            if (originalText.Contains("\n"))
            {
                originalText.Replace("\n", "");
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (buttonText != null)
                buttonText.text = originalText + "<-";
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (buttonText != null)
                buttonText.text = originalText;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buttonText != null)
                buttonText.text = originalText + "<-";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buttonText != null)
                buttonText.text = originalText;
        }
    }
}
