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
        private void Awake()
        {
            _menuCanvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenMenu()
        {
            EventSystem.current.SetSelectedGameObject(_firstSelectedObject.gameObject);
            _menuCanvasGroup.alpha = 1;
            _menuCanvasGroup.blocksRaycasts = true;
            _menuCanvasGroup.interactable = true;
        }
        
        public void CloseMenu()
        {
            _menuCanvasGroup.alpha = 0;
            _menuCanvasGroup.blocksRaycasts = false;
            _menuCanvasGroup.interactable = false;
        }

        public bool IsOpen()
        {
            return _menuCanvasGroup.alpha > 0;
        }
    }
}