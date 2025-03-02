using Player;
using Player.LaserPointer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject _mainMenuCanvasGO;
    private bool isPaused;
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private LaserPointer _laserPointer;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject _resumeFirst;

    private void Start()
    {
        _mainMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        if (MenuInput.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }
    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        _playerControls.enabled = false;
        _laserPointer.enabled = false;
        _playerManager.enabled = false;
        OpenMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        _playerControls.enabled = true;
        _laserPointer.enabled = true;
        _playerManager.enabled = true;
        CloseAllMenus();
    }

    #endregion

    #region Canvas Activations/Deactivations

    private void OpenMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_resumeFirst);
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    #endregion

    public void OnResumePress()
    {
        Unpause();
    }

    public void OnMainMenuPress()
    {

    }

    public void OnQuitPress()
    {
        Application.Quit();
    }
}

