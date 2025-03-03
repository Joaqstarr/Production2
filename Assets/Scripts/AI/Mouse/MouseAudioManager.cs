using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseAudioManager : MonoBehaviour
{    
    public AudioClip rollingSound; 

    private NavMeshAgent agent;
    private AudioSource audioSource;

    private bool _hasMoved = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;

        if (isMoving && !_hasMoved) 
        {
            _hasMoved = true;
            PlayMovementSound();
        }
        else if (!isMoving && _hasMoved) 
        {
            _hasMoved = false;
            StopMovementSound();
        }
    }

    private void PlayMovementSound()
    {
        if (!audioSource.isPlaying) 
        {
            audioSource.clip = rollingSound;
            audioSource.Play();
        }
    }

    private void StopMovementSound()
    {
        audioSource.Stop();
    }
}