using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAudioManager : MonoBehaviour
{
    public static event Action OnFootstep;

    public AudioClip[] footstepSounds;
    public AudioClip[] mouseDieSounds;
    public AudioClip crushMouse;
    public AudioClip crunchSounds;    
    public AudioClip robotJump;
    public AudioClip robotLand;
    public AudioClip powerDownSound;

    public ParticleSystem dustImpact;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f;

       
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

    public void OnCatchStart()
    {
        audioSource.PlayOneShot(robotJump);
    }
    public void PlayCrunch()
    {
        audioSource.PlayOneShot(crunchSounds);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void OnStompHit()
    {
        if (mouseDieSounds.Length > 0)
        {
            AudioClip clip = mouseDieSounds[UnityEngine.Random.Range(0, mouseDieSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
        audioSource.PlayOneShot(crushMouse);
        dustImpact.Play();
    }

    public void OnJumpStart()
    {
        audioSource.PlayOneShot(robotJump);
    }
    public void OnLand()
    {
        audioSource.PlayOneShot(robotLand);
        dustImpact.Play();
    }
    public void OnWin()
    {
        audioSource.PlayOneShot(powerDownSound);
    }
}
