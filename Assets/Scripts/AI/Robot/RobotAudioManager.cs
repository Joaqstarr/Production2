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

    public void PlayCrunch(int index1, int index2)
    {
        if (footstepSounds.Length > 1 && index1 >= 0 && index1 < footstepSounds.Length && index2 >= 0 && index2 < footstepSounds.Length)
        {
            AudioClip clip1 = footstepSounds[index1];
            AudioClip clip2 = footstepSounds[index2];

            audioSource.PlayOneShot(clip1);
            audioSource.PlayOneShot(clip2);
        }
    }

    public void OnStompHit()
    {
        audioSource.PlayOneShot(crushMouse);
    }
}
