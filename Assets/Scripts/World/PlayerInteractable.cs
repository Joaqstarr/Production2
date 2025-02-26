using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractable : MonoBehaviour
{

    public UnityEvent OnInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControls>().InteractPressed += Interact;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        
    }
}
