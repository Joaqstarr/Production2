using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractable : MonoBehaviour
{
    public static string InteractableText;
    public UnityEvent OnInteract;
    [SerializeField] protected string _uiInRangeText;

    PlayerControls playerControls;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractableText = _uiInRangeText;
            playerControls = other.GetComponent<PlayerControls>();
            playerControls.InteractPressed = null;

            playerControls.InteractPressed += Interact;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InteractableText == _uiInRangeText)
            {
                InteractableText = "";
            }


            UnsubscribeFromInput();

        }
    }
    
    
    void Interact()
    {
        UnsubscribeFromInput();

        if (gameObject)
        {
            OnInteract.Invoke();
            PlayerInteractedWith();


        }
    }

    private void UnsubscribeFromInput()
    {
        if (playerControls)
        {
            playerControls.InteractPressed -= Interact;
        }
        playerControls = null;
    }

    virtual protected void PlayerInteractedWith()
    {
        if (InteractableText == _uiInRangeText)
        {
            InteractableText = "";
        }
    }
}
