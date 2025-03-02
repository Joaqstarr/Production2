using System;
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

    private void OnEnable()
    {
        MenuInput.OnTogglePause += OnTogglePause;
    }

    private void OnTogglePause(bool isopen)
    {
        if (isopen)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    private void OnDisable()
    {
        MenuInput.OnTogglePause -= OnTogglePause;

    }

    
    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        OpenMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();
    }

    #endregion

    #region Canvas Activations/Deactivations

    private void OpenMenu()
    {
        _mainMenuCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_resumeFirst);
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;

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

