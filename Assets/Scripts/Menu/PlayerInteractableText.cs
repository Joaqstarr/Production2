using System;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class PlayerInteractableText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
        }


        private void Update()
        {
            _text.text = PlayerInteractable.InteractableText;
        }
    }
}