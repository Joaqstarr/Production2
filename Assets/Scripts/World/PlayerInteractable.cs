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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractableText = _uiInRangeText;
            other.GetComponent<PlayerControls>().InteractPressed += Interact;
            
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
            other.GetComponent<PlayerControls>().InteractPressed -= Interact;
        }
    }
    
    
    void Interact()
    {
        if (gameObject)
        {
            OnInteract.Invoke();
            PlayerInteractedWith();
        }
    }

    virtual protected void PlayerInteractedWith()
    {
        if (InteractableText == _uiInRangeText)
        {
            InteractableText = "";
        }
    }
}
