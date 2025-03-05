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
        private static readonly int WinTrigger = Animator.StringToHash("Win");


        public delegate void CutsceneEventDelegate();

        public static CutsceneEventDelegate OnOpenElectricalBox;
        public static CutsceneEventDelegate OnFinishWinAnimation;

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
            MenuManager.OnGameStart += OnGameStart;
        }

        private void OnGameStart()
        {
            CutsceneManager.StartCutscene(CutsceneManager.Cutscenes.ReadyGame, transform);
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
                case CutsceneManager.Cutscenes.Win:
                    PlayWinCutscene(location);
                    break;
                    
            }
        }

        void PlayWinCutscene(Transform pos)
        {
            _characterController.enabled = false;
            transform.position = pos.position;
            transform.rotation = pos.rotation;
            _animator.SetTrigger(WinTrigger);

        }
        private void OnDisable()
        {
            CutsceneManager.OnPlayCutscene -= OnPlayCutscene;
            CutsceneManager.OnEndCutscene -= OnEndCutscene;
            MenuManager.OnGameStart -= OnGameStart;

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
            CutsceneManager.EndCutscene(CutsceneManager.Cutscenes.ReadyGame);
        }

        public void OpenElectrical()
        {
            OnOpenElectricalBox?.Invoke();
        }

        public void FinishWin()
        {
            OnFinishWinAnimation?.Invoke();

        }
    }
}
