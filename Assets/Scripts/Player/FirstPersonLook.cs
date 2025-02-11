using System;
using UnityEngine;

namespace Player
{
    public class FirstPersonLook : MonoBehaviour
    {
        private bool _isLookEnabled = true;
        private PlayerControls _playerControls;

        [SerializeField] private Vector2 _lookSensitivity = Vector2.one;
        [SerializeField] private Transform _verticalRotationTransform;
        private void Start()
        {
            _playerControls = GetComponent<PlayerControls>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (_isLookEnabled && _playerControls)
            {
                AddHorizontalInput(_playerControls.LookInput.x);            
                AddVerticalInput(_playerControls.LookInput.y);            
            }
        }

        void AddHorizontalInput(float input)
        {
            transform.RotateAround(transform.position, transform.up, input * _lookSensitivity.x);
        }

        void AddVerticalInput(float input)
        {
            _verticalRotationTransform.RotateAround(_verticalRotationTransform.position, _verticalRotationTransform.right, input * _lookSensitivity.y);
        }
    }
}