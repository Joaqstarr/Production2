using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour
{
    public static MenuInput instance;
    public bool MenuOpenCloseInput { get; private set; }
    private MenuInput _menuInput;
    private InputAction _menuOpenCloseAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _menuInput = GetComponent<PlayerInput>();
        _menuOpenCloseAction = _menuInput.action["MenuOpenClose"];
    }

    private void Update()
    {
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
    }
}
