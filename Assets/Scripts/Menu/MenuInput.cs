using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    public bool IsMenuOpen { get; private set; }
    private PlayerInput _playerInput;
    
    public delegate void PauseDelegate(bool isOpen);
    public static PauseDelegate OnTogglePause;

    private void Awake()
    {

    }



    private void OnToggleMenu(InputValue value)
    {

        IsMenuOpen = !IsMenuOpen;


        OnTogglePause?.Invoke(IsMenuOpen);
    }
}
