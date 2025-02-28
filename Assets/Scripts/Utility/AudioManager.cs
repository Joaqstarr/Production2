using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] idleSounds;
    public AudioClip[] footstepSounds;
    public AudioClip[] crunchSounds;

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
            audioSource.clip = idleSounds[Random.Range(0, idleSounds.Length)];
            audioSource.Play();
        }
    }

    public void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
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
}
