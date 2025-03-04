using AI.Sensing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseAudioManager : MonoBehaviour
{    
    public AudioClip rollingSound;
    public AudioClip alertSound;
    public AudioClip spawnSound;

    private NavMeshAgent agent;
    private AudioSource audioSource;

    private bool _hasMoved = false;
    private GenericDistanceListener _laserListener;

    private float cooldown = 2.0f; 
    private float lastAlert = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        _laserListener = new GenericDistanceListener(SenseNotificationContext.NotificationType.Laser, 5, transform);
        _laserListener.OnNotificationReceived += OnLaserReceived;
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

    private void OnLaserReceived(SenseNotificationContext notification)
    {
        if (Time.time - lastAlert >= cooldown)
        {
            audioSource.PlayOneShot(alertSound);
            lastAlert = Time.time;
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

    //void OnEnable()
    //{
    //    audioSource.PlayOneShot(spawnSound);
    //}
}