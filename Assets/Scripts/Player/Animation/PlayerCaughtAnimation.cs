using AI.Robot.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Animation
{
    public class PlayerCaughtAnimation : MonoBehaviour
    {
        private static readonly int CaughtTrigger = Animator.StringToHash("Caught");

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

            _characterController.enabled = false;

            Vector3 dir = (transform.position - robot.position).normalized;
            transform.position = robot.position + (dir * _distance);
            transform.rotation = robot.rotation;

            _animator.SetTrigger(CaughtTrigger);

        }
    }
}
