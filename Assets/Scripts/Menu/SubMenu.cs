using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class SubMenu : MonoBehaviour
    {
        
        private CanvasGroup _menuCanvasGroup;
        [SerializeField] private Selectable _firstSelectedObject;
        [SerializeField] private float _fadeTime = 0;
        private void Awake()
        {
            _menuCanvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenMenu()
        {
            EventSystem.current.SetSelectedGameObject(_firstSelectedObject.gameObject);
            if(_fadeTime <= 0)
            {
                _menuCanvasGroup.alpha = 1;
                _menuCanvasGroup.blocksRaycasts = true;
                _menuCanvasGroup.interactable = true;
                return;
            }

            _menuCanvasGroup.DOFade(1, _fadeTime).onComplete += () =>
            {
                _menuCanvasGroup.blocksRaycasts = true;
                _menuCanvasGroup.interactable = true;

            };

        }
        
        public void CloseMenu()
        {
            if (_fadeTime <= 0)
            {
                _menuCanvasGroup.alpha = 0;
                _menuCanvasGroup.blocksRaycasts = false;
                _menuCanvasGroup.interactable = false;
                return;
            }

            _menuCanvasGroup.DOFade(0, _fadeTime).onComplete += () =>
            {
                _menuCanvasGroup.blocksRaycasts = false;
                _menuCanvasGroup.interactable = false;

            };
        }

        public bool IsOpen()
        {
            return _menuCanvasGroup.alpha > 0;
        }
    }
}