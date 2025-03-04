using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    public delegate void PauseDelegate();
    public static PauseDelegate OnTogglePause;

    private void Awake()
    {

    }



    private void OnToggleMenu(InputValue value)
    {



        OnTogglePause?.Invoke();
    }
}
