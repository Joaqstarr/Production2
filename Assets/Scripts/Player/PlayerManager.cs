using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private CharacterController _characterController;

        [Header("Scriptable Object Settings")]
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private LookSettings lookSettings;
        [SerializeField] private GravitySettings gravitySettings;
        [SerializeField] private CrouchSettings crouchSettings;
        [SerializeField] private SprintSettings sprintSettings;
        [SerializeField] private JumpSettings jumpSettings;

        [Header("Camera Setup")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Camera playerCamera;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckDistance = 0.3f;
        [SerializeField] private LayerMask groundLayer;

        private bool _isGrounded;

        [SerializeField] private Volume globalVolume;
        private Vignette _vignette;

        private float _currentGravity = 0;
        private Vector2 _movementInput;
        private Vector2 _lookInput;

        private bool _isSprinting = false;
        private bool _isCrouching = false;
        private float _targetHeight;
        private float _initialCameraY;
        private float _targetCameraY;

        private void Start()
        {
            _playerControls = GetComponent<PlayerControls>();
            _characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;

            _initialCameraY = cameraTransform.localPosition.y;
            _targetCameraY = _initialCameraY; 

            _targetHeight = crouchSettings.standingHeight;

            if (globalVolume.profile.TryGet(out Vignette vignette))
            {
                _vignette = vignette;
            }
        }

        private void Update()
        {
            if (_playerControls)
            {
                _movementInput = _playerControls.MovementInput;
                _lookInput = _playerControls.LookInput;

                HandleJumping();
                HandleMovement();
                HandleLook();
                HandleCrouching();
                HandleSprinting();                
            }
        }

        private void HandleMovement()
        {
            Vector3 forwardMove = transform.forward * _movementInput.y;
            Vector3 rightMove = transform.right * _movementInput.x;

            Vector3 movementDir = (forwardMove + rightMove).normalized;

            _currentGravity += gravitySettings.gravity * Time.deltaTime;

            movementDir.y = _currentGravity;

            float speed = movementSettings.walkSpeed;

            if (_isCrouching)
            {
                speed = crouchSettings.crouchSpeed;
            }

            if (_isSprinting)
            {
                speed = sprintSettings.sprintSpeed;
            }

            _characterController.Move(movementDir * (Time.deltaTime * speed));
        }

        private void HandleLook()
        {
            transform.RotateAround(transform.position, transform.up, _lookInput.x * lookSettings.lookSensitivity.x);

            float newRotation = cameraTransform.localEulerAngles.x - _lookInput.y * lookSettings.lookSensitivity.y;

            if (newRotation > 180f) newRotation -= 360f;

            newRotation = Mathf.Clamp(newRotation, lookSettings.minMaxVerticalLook.x, lookSettings.minMaxVerticalLook.y);

            cameraTransform.localRotation = Quaternion.Euler(newRotation, 0f, 0f);
        }

        private void HandleCrouching()
        {
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                if (_isSprinting)
                {
                    _isSprinting = false;
                }

                _isCrouching = !_isCrouching;
                _targetHeight = _isCrouching ? crouchSettings.crouchHeight : crouchSettings.standingHeight;

                _targetCameraY = _isCrouching
                    ? _initialCameraY - crouchSettings.cameraCrouchOffset
                    : _initialCameraY;
            }

            float currentHeight = _characterController.height;
            _characterController.height = Mathf.Lerp(currentHeight, _targetHeight, Time.deltaTime * crouchSettings.crouchTransitionSpeed);

            Vector3 cameraPosition = cameraTransform.localPosition;
            cameraPosition.y = Mathf.Lerp(cameraPosition.y, _targetCameraY, Time.deltaTime * crouchSettings.crouchTransitionSpeed);
            cameraTransform.localPosition = cameraPosition;



            if (_vignette != null)
            {
                float targetVignetteIntensity = _isCrouching ? 0.25f : 0f;
                _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, targetVignetteIntensity, Time.deltaTime * 5f);
            }
        }

        private void HandleSprinting()
        {
            _isSprinting = Keyboard.current.leftShiftKey.isPressed && _movementInput != Vector2.zero && !_isCrouching; 

            float targetFOV = _isSprinting ? sprintSettings.sprintFOV : sprintSettings.normalFOV;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * sprintSettings.zoomSpeed);
        }

        //Note to self: Jump does not work when on object edge due to raycast. Find alternative method
        private void HandleJumping()
        {
            Debug.DrawRay(groundCheckPoint.position, Vector3.down * groundCheckDistance, Color.red);

            _isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);

            if (_isGrounded)
            {
                _currentGravity = -2f;

                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    _currentGravity = Mathf.Sqrt(jumpSettings.jumpHeight * -2f * gravitySettings.gravity);
                }
            }
            else
            {
                if (_currentGravity < 0)
                {
                    _currentGravity += (gravitySettings.gravity * (jumpSettings.fallMultiplier - 1)) * Time.deltaTime;
                }
                else if (_currentGravity > 0 && !Keyboard.current.spaceKey.isPressed)
                {
                    _currentGravity += (gravitySettings.gravity * (jumpSettings.lowJumpMultiplier - 1)) * Time.deltaTime;
                }

                _currentGravity += gravitySettings.gravity * Time.deltaTime;
            }
        }
    }
}