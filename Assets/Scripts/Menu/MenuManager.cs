using System;
using Player;
using Player.LaserPointer;
using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup _menuCanvasGroup;
    public SubMenu _pauseScreenSubmenu;
    public SubMenu _settingsMenuSubmenu;
    public SubMenu _titleScreenMenu;
    
    public delegate void MenuDelegate();
    public static MenuDelegate OnGameStart;
    
    private bool isPaused;

    private void Start()
    {
        DisableCanvasGroup(_menuCanvasGroup);

        _pauseScreenSubmenu.CloseMenu();
        _settingsMenuSubmenu.CloseMenu();
        OpenTitleScreen();
    }

    public void OpenTitleScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;

        DisableCanvasGroup(_menuCanvasGroup);
        _titleScreenMenu.OpenMenu();
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _titleScreenMenu.CloseMenu();
        OnGameStart?.Invoke();
    }

    private static void DisableCanvasGroup(CanvasGroup group)
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }
    private static void EnableCanvasGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }
    private void OnEnable()
    {
        MenuInput.OnTogglePause += OnTogglePause;
    }

    private void OnTogglePause()
    {
        if (_titleScreenMenu.IsOpen()) return;
        
        if (isPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
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

        CloseAllPauseMenus();
    }

    #endregion

    #region Canvas Activations/Deactivations

    private void OpenMenu()
    {
        EnableCanvasGroup(_menuCanvasGroup);
        _settingsMenuSubmenu.CloseMenu();
        _pauseScreenSubmenu.OpenMenu();

        Cursor.lockState = CursorLockMode.None;
    }

    private void OpenSettingsMenu()
    {
        EnableCanvasGroup(_menuCanvasGroup);
        _pauseScreenSubmenu.CloseMenu();
        _settingsMenuSubmenu.OpenMenu();
        

        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseAllPauseMenus()
    {
        DisableCanvasGroup(_menuCanvasGroup);
        _settingsMenuSubmenu.CloseMenu();
        _pauseScreenSubmenu.OpenMenu();
        
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

    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

