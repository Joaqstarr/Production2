using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player.Animation
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private static readonly int ForwardSpeedProperty = Animator.StringToHash("ForwardSpeed");
        private static readonly int RightSpeedProperty = Animator.StringToHash("RightSpeed");
        private static readonly int IsMovingProperty = Animator.StringToHash("IsMoving");
        private static readonly int CrouchInterpProperty = Animator.StringToHash("CrouchInterp");

        private Animator _animator;
        private CharacterController _characterController;
        private PlayerManager _playerManager;
        
        [SerializeField] private float _transitionSpeed = 5;

        private float _crouchTransitionSpeed = 1;
        private bool _isCrouching = false;
        private Tweener _crouchTweener;
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _playerManager = GetComponent<PlayerManager>();
            _crouchTransitionSpeed = _playerManager.GetCrouchTransitionSpeed();
        }

        // Update is called once per frame
        void Update()
        {
            HandleLocomotion();
            
            if (_playerManager.IsCrouching() != _isCrouching)
            {
                ToggleCrouch();
            }
        }

        private void ToggleCrouch()
        {
            Debug.Log("Crouch toggle");
            float time = _crouchTransitionSpeed;
                
            //subtract current time from time to apply
            if (_crouchTweener != null && _crouchTweener.IsPlaying())
            {
                time -= Mathf.Lerp(0, _crouchTransitionSpeed, _crouchTweener.position);
                _crouchTweener.Kill();
            }
            float start = 0;
            float end = 1;
            if (!_playerManager.IsCrouching())
            {
                start = 1;
                end = 0;
            }

            _crouchTweener = DOVirtual.Float(start, end, 1/time, (float interp) =>
            {
                _animator.SetFloat(CrouchInterpProperty, interp);
            });

            _crouchTweener.Play();
                
            _isCrouching = _playerManager.IsCrouching();
        }

        private void HandleLocomotion()
        {
            Vector3 vel = _characterController.velocity;


            float oldForwardSpeed = _animator.GetFloat(ForwardSpeedProperty);
            float oldRightSpeed = _animator.GetFloat(RightSpeedProperty);


            float newForwardSpeed = Vector3.Dot(transform.forward, vel);
            float newRightSpeed = Vector3.Dot(transform.right, vel);

            float forwardSpeed = Mathf.Lerp(oldForwardSpeed, newForwardSpeed, Time.deltaTime * _transitionSpeed);
            float rightSpeed = Mathf.Lerp(oldRightSpeed, newRightSpeed, Time.deltaTime * _transitionSpeed);

            if (Mathf.Abs(forwardSpeed) < 0.1)
            {
                forwardSpeed = 0;
            }

            if (Mathf.Abs(rightSpeed) < 0.1)
            {
                rightSpeed = 0;
            }

            _animator.SetFloat(ForwardSpeedProperty, forwardSpeed);
            _animator.SetFloat(RightSpeedProperty, rightSpeed);
            _animator.SetBool(IsMovingProperty, vel.magnitude > 0.1f);
        }
    }
}