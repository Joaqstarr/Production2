using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAudioManager : MonoBehaviour
{
    public AudioClip[] idleSounds;
    public AudioClip[] alertSounds;

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
}
