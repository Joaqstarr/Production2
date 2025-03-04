using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Fader : MonoBehaviour
    {
        public static Fader Instance;

        private CanvasGroup _canvasGroup;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            Instance = this;
            
        }

        private void Start()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0, 0.5f);
        }

        
        public void CutToBlack()
        {
            _canvasGroup.alpha = 1;
        }
    }
}