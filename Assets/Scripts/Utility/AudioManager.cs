using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static event Action OnFootstep;

    [Header("Gameplay Sounds")]
    public AudioClip[] idleSounds;
    public AudioClip[] footstepSounds;
    public AudioClip piperScream;
    public AudioClip laserClick;
    

    [Header("Intro Sounds")]
    public AudioClip piperJump;
    public AudioClip landSound;

    [Header("Win Game Sounds")]
    [SerializeField]
    private AudioClip[] piperPunchSounds;
    [SerializeField]
    private AudioClip[] piperGruntSounds;
    [SerializeField]
    private AudioClip[] hitElectricalBoxSounds;
    public AudioClip openElectricalSound;
    public AudioClip scareSound;
    public AudioClip breakBoxSound;


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

    public void OnPickedUp()
    {
        audioSource.PlayOneShot(piperScream);
    }
    public void LaserClick()
    {
        audioSource.PlayOneShot(laserClick);
    }

    //Intro sounds
    public void OnPiperJump()
    {
        audioSource.PlayOneShot(piperJump);
    }
    public void IntroLand()
    {
        audioSource.PlayOneShot(landSound);
    }


    //Win game sounds
    public void PlayRandomPunch()
    {
        //if (piperPunchSounds.Length > 0)
        //{
        //    int index = UnityEngine.Random.Range(0, piperPunchSounds.Length);

        //    audioSource.PlayOneShot(piperPunchSounds[index]);
        //}
        
        if (hitElectricalBoxSounds.Length > 0)
        {
            int index = UnityEngine.Random.Range(0, hitElectricalBoxSounds.Length);

            audioSource.PlayOneShot(hitElectricalBoxSounds[index]);
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

    public void OnOpenElectrical()
    {
        audioSource.PlayOneShot(openElectricalSound);
    }
    public void OnBreakBox()
    {
        audioSource.PlayOneShot(breakBoxSound);
    }
    public void OnScared()
    {
        audioSource.PlayOneShot(scareSound);
    }
}
