using System;
using System.Collections;
using System.Collections.Generic;
using AI.Robot.Animation;
using Cinemachine;
using UnityEngine;
using World;

namespace Player.Animation
{

    public class CutsceneCam : MonoBehaviour{
        private static readonly int Caught = Animator.StringToHash("Caught");
        
        private static readonly int StartTrigger = Animator.StringToHash("TriggerStartScreen");
        private static readonly int OnStartTrigger = Animator.StringToHash("OnStartGame");

        
        private Animator _animator;

        private CinemachineVirtualCamera _virtualCamera;


        private void OnEnable()
        {
            _animator = GetComponentInChildren<Animator>();
            _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            CutsceneManager.OnPlayCutscene += OnPlayCutscene;
            CutsceneManager.OnEndCutscene += OnEndCutscene;
        }

        private void OnEndCutscene(CutsceneManager.Cutscenes cutscene)
        {
            _virtualCamera.Priority = -1;
        }

        private void OnPlayCutscene(CutsceneManager.Cutscenes cutscene, Transform location)
        {
            _virtualCamera.Priority = 11;
            switch (cutscene)
            {
                case CutsceneManager.Cutscenes.Caught:
                    PlayCutsceneAtLocation(location, Caught);
                    break;
                case CutsceneManager.Cutscenes.StartGame:

                    transform.position = location.position;
                    transform.rotation = location.rotation;
                    //flip around
                    transform.eulerAngles += new Vector3(0, 180, 0);
                    PlayCutscene(StartTrigger);

                    break;
                case CutsceneManager.Cutscenes.ReadyGame:
                    PlayCutscene(OnStartTrigger);
                    break;
            }
            
        }

        private void OnDisable()
        {
            CutsceneManager.OnPlayCutscene -= OnPlayCutscene;
            CutsceneManager.OnEndCutscene -= OnEndCutscene;

        }

        private void PlayCutsceneAtLocation(Transform location, int cutsceneId)
        {
            transform.position = location.position;
            transform.rotation = location.rotation;
            PlayCutscene(cutsceneId);
        }

        private void PlayCutscene(int cutsceneId)
        {
            _animator.SetTrigger(cutsceneId);
        }
    }

}