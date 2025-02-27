using System;
using System.Collections;
using System.Collections.Generic;
using AI.Robot.Animation;
using Cinemachine;
using UnityEngine;

namespace Player.Animation
{

    public class CutsceneCam : MonoBehaviour{
        private static readonly int Caught = Animator.StringToHash("Caught");

        private Animator _animator;

        private CinemachineVirtualCamera _virtualCamera;
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        private void OnEnable()
        {
            CatchCutscene.OnCaughtCutscneTriggered += PlayCaughtCutscene;
        }

        private void OnDisable()
        {
            CatchCutscene.OnCaughtCutscneTriggered -= PlayCaughtCutscene;
        }

        public void PlayCaughtCutscene(Transform robotPosition)
        {
            transform.position = robotPosition.position;
            transform.rotation = robotPosition.rotation;
            _virtualCamera.Priority = 11;
            _animator.SetTrigger(Caught);
        }
    }

}