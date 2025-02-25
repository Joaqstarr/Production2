using AI.Robot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Animation
{
    public class PlayerCaughtAnimation : MonoBehaviour
    {
        private static readonly int CaughtTrigger = Animator.StringToHash("Caught");

        private Animator _animator;
        private PlayerManager _playerManager;
        private void Awake()
        {
            _playerManager = GetComponent<PlayerManager>();
            _animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            CatchCutscene.OnCaughtCutscneTriggered += CutsceneTriggered;
        }
        private void OnDisable()
        {
            CatchCutscene.OnCaughtCutscneTriggered -= CutsceneTriggered;
        }

        private void CutsceneTriggered(Transform robot)
        {
            // Disable player movement and AI logic
            _playerManager.enabled = false;

            transform.position = robot.position;
            transform.rotation = robot.rotation;

            _animator.SetTrigger(CaughtTrigger);

        }
    }
}
