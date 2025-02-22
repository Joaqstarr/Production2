using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private static readonly int ForwardSpeedProperty = Animator.StringToHash("ForwardSpeed");
    private static readonly int RightSpeedProperty = Animator.StringToHash("RightSpeed");
    private static readonly int IsMovingProperty = Animator.StringToHash("IsMoving");

    private Animator _animator;
    private CharacterController _characterController;

    [SerializeField] private float _transitionSpeed = 5;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
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
        
        if(Mathf.Abs(rightSpeed) < 0.1)
        {
            rightSpeed = 0;
        }
        
        _animator.SetFloat(ForwardSpeedProperty, forwardSpeed);
        _animator.SetFloat(RightSpeedProperty, rightSpeed);
        _animator.SetBool(IsMovingProperty, vel.magnitude > 0.1f);
    }
}
