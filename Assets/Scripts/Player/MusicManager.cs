using System;
using DG.Tweening;
using UnityEngine;
using World;

namespace Player
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _ambientMusicSource;
        [SerializeField] private AudioSource _spottedMusicSource;

        private void OnEnable()
        {
            CutsceneManager.OnPlayCutscene += OnPlayCutscene;
        }

        private void OnDisable()
        {
            CutsceneManager.OnPlayCutscene -= OnPlayCutscene;

        }

        private void OnPlayCutscene(CutsceneManager.Cutscenes cutscene, Transform location)
        {
            if (cutscene == CutsceneManager.Cutscenes.ReadyGame)
            {
                _ambientMusicSource.Play();
                _ambientMusicSource.volume = 0;
                _ambientMusicSource.DOFade(1, 4f);
                _spottedMusicSource.Play();
                _spottedMusicSource.volume = 0;
            }
        }
    }
}