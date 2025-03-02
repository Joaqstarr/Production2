using System;
using AI.Robot.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;


namespace Player.Animation
{
    public class PlayerCutsceneManager : MonoBehaviour
    {
        private static readonly int CaughtTrigger = Animator.StringToHash("Caught");
        private static readonly int StartTrigger = Animator.StringToHash("TriggerStartScreen");
        private static readonly int ReadyTrigger = Animator.StringToHash("OnStartGame");

        
        [SerializeField]
        private float _distance = 0.8f;
        private Animator _animator;
        private PlayerManager _playerManager;
        private CharacterController _characterController;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerManager = GetComponent<PlayerManager>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {

        }

        private void OnEnable()
        {
            
            CutsceneManager.OnPlayCutscene += OnPlayCutscene;
            CutsceneManager.OnEndCutscene += OnEndCutscene;
        }

        private void OnEndCutscene(CutsceneManager.Cutscenes cutscene)
        {
        }

        private void OnPlayCutscene(CutsceneManager.Cutscenes cutscene, Transform location)
        {
           // _characterController.enabled = false;

            switch (cutscene)
            {
                case CutsceneManager.Cutscenes.Caught:
                    PlayCaughtCutscene(location);
                    break;
                case CutsceneManager.Cutscenes.StartGame:
                    PlayStartCutscene(location);
                    break;
                case CutsceneManager.Cutscenes.ReadyGame:
                    PlayReadyGameCutscene();
                    break;
                    
            }
        }

        private void OnDisable()
        {
            CutsceneManager.OnPlayCutscene -= OnPlayCutscene;
            CutsceneManager.OnEndCutscene -= OnEndCutscene;
        }

        void PlayReadyGameCutscene()
        {
            _animator.SetTrigger(ReadyTrigger);

        }
        private void PlayStartCutscene(Transform root)
        {

            transform.position = root.position;
            transform.rotation = root.rotation;
            
            _animator.SetTrigger(StartTrigger);

        }
        private void PlayCaughtCutscene(Transform robot)
        {
            // Disable player movement and AI logic
            //_playerManager.enabled = false;


            Vector3 dir = (transform.position - robot.position).normalized;
            transform.position = robot.position + (dir * _distance);
            transform.rotation = robot.rotation;

            _animator.SetTrigger(CaughtTrigger);

        }

        private void FinishReady()
        {
            Debug.Log("Finish Ready!!");
            CutsceneManager.EndCutscene(CutsceneManager.Cutscenes.ReadyGame);
        }
    }
}
