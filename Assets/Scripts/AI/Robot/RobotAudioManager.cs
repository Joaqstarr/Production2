using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAudioManager : MonoBehaviour
{
    public static event Action OnFootstep;

    public AudioClip[] idleSounds;
    public AudioClip[] footstepSounds;
    public AudioClip[] crunchSounds;
    public AudioClip crushMouse;
    public AudioClip robotJump;
    public AudioClip robotLand;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f;

        PlayIdleLoop();
    }
    public void PlayIdleLoop()
    {
        if (idleSounds.Length > 0)
        {
            audioSource.clip = idleSounds[UnityEngine.Random.Range(0, idleSounds.Length)];
            audioSource.Play();
        }
    }

    public void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }

        OnFootstep?.Invoke();
    }

    public void PlayCrunch()
    {

            AudioClip clip1 = crunchSounds[0];
            AudioClip clip2 = crunchSounds[1];

            audioSource.PlayOneShot(clip1);
            audioSource.PlayOneShot(clip2);
        
    }

    public void OnStompHit()
    {
        audioSource.PlayOneShot(crushMouse);
    }

    public void OnJumpStart()
    {
        audioSource.PlayOneShot(robotJump);
    }
    public void OnLand()
    {
        audioSource.PlayOneShot(robotLand);
    }
}
