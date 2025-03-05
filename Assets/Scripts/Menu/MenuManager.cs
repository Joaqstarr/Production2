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
using World;
using Player.Animation;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup _menuCanvasGroup;
    public SubMenu _pauseScreenSubmenu;
    public SubMenu _settingsMenuSubmenu;
    public SubMenu _titleScreenMenu;
    public SubMenu _winScreenMenu;

    public delegate void MenuDelegate();
    public static MenuDelegate OnGameStart;
    
    private bool isPaused;

    private void Start()
    {
        DisableCanvasGroup(_menuCanvasGroup);

        _pauseScreenSubmenu.CloseMenu();
        _settingsMenuSubmenu.CloseMenu();
        
    }

    private void Awake()
    {
        CutsceneManager.OnPlayCutscene += OnPlayCutscene;

    }

    private void OnPlayCutscene(CutsceneManager.Cutscenes cutscene, Transform location)
    {
        if(cutscene == CutsceneManager.Cutscenes.StartGame)
        {
            OpenTitleScreen();
        }
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
        PlayerCutsceneManager.OnFinishWinAnimation += TriggerWinScreen;
    }

    private void OnTogglePause()
    {
        if (_titleScreenMenu.IsOpen() || _winScreenMenu.IsOpen()) return;
        
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
        PlayerCutsceneManager.OnFinishWinAnimation -= TriggerWinScreen;

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
        StartCoroutine(ReloadScene());
    }
    IEnumerator ReloadScene()
    {

        if (Fader.Instance)
        {
            Fader.Instance.CutToBlack();
        }
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.5f);
        CutsceneManager.EndCutscene(CutsceneManager.Cutscenes.Win);

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TriggerWinScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        DisableCanvasGroup(_menuCanvasGroup);
        _titleScreenMenu.CloseMenu();
        _winScreenMenu.OpenMenu();
    }
}

