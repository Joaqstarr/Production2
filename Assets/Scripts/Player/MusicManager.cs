using System;
using AI.Drone.States.SearchStates;
using DG.Tweening;
using UnityEngine;
using World;

namespace Player
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _ambientMusicSource;
        [SerializeField] private AudioSource _spottedMusicSource;
        [SerializeField] private float _fadeSpeed = 1;
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

        private void Update()
        {
            float targetVolume = LookState.AmountSpotted > 0 ? 1 : 0;
            
            _spottedMusicSource.volume = Mathf.MoveTowards(_spottedMusicSource.volume, targetVolume, Time.deltaTime *_fadeSpeed);
        }
    }
}