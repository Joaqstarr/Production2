using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static event Action OnFootstep;

    public AudioClip[] idleSounds;
    public AudioClip[] footstepSounds;
    public AudioClip piperJump;    
    public AudioClip landSound;
    public AudioClip piperScream;
    public AudioClip laserClick;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] piperPunchSounds;
    [SerializeField]
    private AudioClip[] piperGruntSounds;

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

    public void OnPiperJump()
    {
        audioSource.PlayOneShot(piperJump);
    }

    public void OnPickedUp()
    {
        audioSource.PlayOneShot(piperScream);
    }

    public void LaserClick()
    {
        audioSource.PlayOneShot(laserClick);
    }
    public void IntroLand()
    {
        audioSource.PlayOneShot(landSound);
    }

    public void PlayRandomPunch()
    {
        if (piperPunchSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, piperPunchSounds.Length);

            audioSource.PlayOneShot(piperPunchSounds[index]);
        }
    }

    public void PlayRandomGrunt()
    {
        if (piperPunchSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, piperGruntSounds.Length);

            audioSource.PlayOneShot(piperPunchSounds[index]);
        }
    }
}
