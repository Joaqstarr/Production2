using System;
using Player;
using Player.LaserPointer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public GameObject _mainMenuCanvasGO;
    public GameObject _settingsMenuCanvasGO;
    private bool isPaused;
    [FormerlySerializedAs("_resumeFirst")] [SerializeField] private GameObject _firstSelectedButton;

    private void Start()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
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
        _settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton);
        Cursor.lockState = CursorLockMode.None;
    }

    private void OpenSettingsMenu()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_firstSelectedButton);
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseAllMenus()
    {
        _mainMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Cursor.lockState = CursorLockMode.Locked;

    }

    #endregion

    public void OnResumePress()
    {
        Unpause();
    }

    public void OnSettingsMenuPress()
    {
        OpenSettingsMenu();
    }

    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnSettingsBackPress()
    {
        OpenMenu();
    }
}

