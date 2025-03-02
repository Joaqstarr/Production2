using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using Player.LaserPointer;
using World;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private CharacterController _characterController;
        private Animator _animator;

        [Header("Scriptable Object Settings")] [SerializeField] 
        private MovementSettings movementSettings;

        [SerializeField] private LookSettings lookSettings;
        [SerializeField] private GravitySettings gravitySettings;
        [SerializeField] private CrouchSettings crouchSettings;
        [SerializeField] private SprintSettings sprintSettings;
        [SerializeField] private JumpSettings jumpSettings;

        [Header("Camera Setup")] [SerializeField]
        private Transform cameraTransform;

        [SerializeField] private CinemachineVirtualCamera playerCamera;

        [Header("Ground Check")] 
        [SerializeField]
        private Transform groundCheckPoint;

        [SerializeField] private float groundCheckDistance = 0.3f;
        [SerializeField] private LayerMask groundLayer;

        private bool _isGrounded;

        [Header("Footstep Particle")]
        [SerializeField] private ParticleSystem footstepEffect;

        [SerializeField]
        private LaserPointer.LaserPointer _laserPointer;
        [SerializeField] private GameObject sprintEffectObject;
        [SerializeField] private Volume globalVolume;
        private Vignette _vignette;

        private float _currentGravity = 0;
        private Vector2 _movementInput;
        private Vector2 _lookInput;

        private bool _isSprinting = false;
        private bool _isCrouching = false;
        private bool _uncrouchOstructed = false;
        
        private float _targetHeight;
        private float _initialCameraY;
        private float _targetCameraY;

        
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _playerControls = GetComponent<PlayerControls>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();


        }

        private void Start()
        {

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
            if (!CutsceneManager.IsInCutscene&& _playerControls)
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

            if (_isCrouching || _uncrouchOstructed)
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

            newRotation = Mathf.Clamp(newRotation, lookSettings.minMaxVerticalLook.x,
                lookSettings.minMaxVerticalLook.y);

            cameraTransform.localRotation = Quaternion.Euler(newRotation, 0f, 0f);
        }

        private void HandleCrouching()
        {
            if (_uncrouchOstructed)
            {
                if (!IsObstructedAbove())
                {
                    _uncrouchOstructed = false;
                }
                else
                {
                    return;
                }
            }


            float currentHeight = _characterController.height;
            _characterController.height = Mathf.Lerp(currentHeight, _targetHeight,
                Time.deltaTime * crouchSettings.crouchTransitionSpeed);

            Vector3 cameraPosition = cameraTransform.localPosition;
            cameraPosition.y = Mathf.Lerp(cameraPosition.y, _targetCameraY,
                Time.deltaTime * crouchSettings.crouchTransitionSpeed);
            cameraTransform.localPosition = cameraPosition;



            if (_vignette != null)
            {
                float targetVignetteIntensity = _isCrouching ? 0.25f : 0f;
                _vignette.intensity.value =
                    Mathf.Lerp(_vignette.intensity.value, targetVignetteIntensity, Time.deltaTime * 5f);
            }
        }

        private void HandleSprinting()
        {
            bool wasSprinting = _isSprinting;
            _isSprinting = Keyboard.current.leftShiftKey.isPressed && _movementInput != Vector2.zero && !_isCrouching;

            float targetFOV = _isSprinting ? sprintSettings.sprintFOV : sprintSettings.normalFOV;
            playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, targetFOV,
                Time.deltaTime * sprintSettings.zoomSpeed);

            if (sprintEffectObject != null)
            {
                sprintEffectObject.SetActive(_isSprinting);
            }

            _animator.speed = _isSprinting ? 1.3f : 1f;
        }


        private void HandleJumping()
        {
            //Debug.DrawRay(groundCheckPoint.position, Vector3.down * groundCheckDistance, Color.red);

            _isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);

            if (_isGrounded && _currentGravity < 0)
            {
                _currentGravity = -2f;
            }

            /*if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
            {
                _currentGravity = Mathf.Sqrt(jumpSettings.jumpHeight * -2f * gravitySettings.gravity);
            }*/
        }

        private void OnEnable()
        {
            AudioManager.OnFootstep += PlayFootstepEffect; 
            _playerControls.OnCrouchPressed += OnCrouchPressed;
        }

        private void OnCrouchPressed()
        {

            if (_isCrouching)
            {
                _isCrouching = false;

                if (IsObstructedAbove())
                {
                    _uncrouchOstructed = true;
                }
            }
            else
            {
                _isCrouching = true;
                
            }
            
            _targetHeight = _isCrouching ? crouchSettings.CrouchColliderHeight : crouchSettings.StandColliderHeight;

            _targetCameraY = _isCrouching
                ? _initialCameraY - crouchSettings.cameraCrouchOffset
                : _initialCameraY;
        }
        private bool IsObstructedAbove()
        {
            float checkHeight = crouchSettings.standingHeight - crouchSettings.crouchHeight;
            Vector3 start = transform.position + Vector3.up * crouchSettings.crouchHeight;
            Vector3 end = transform.position + Vector3.up * crouchSettings.standingHeight;

            return Physics.CheckCapsule(start, end, _characterController.radius, groundLayer);
        }

        private void OnDisable()
        {
            _playerControls.OnCrouchPressed -= OnCrouchPressed;

            AudioManager.OnFootstep -= PlayFootstepEffect;
        }
        private void PlayFootstepEffect()
        {
            if (_isGrounded && footstepEffect != null)
            {
                footstepEffect.Play();
            }
        }


        public bool IsCrouching()
        {
            return _isCrouching || _uncrouchOstructed;
        }

        public float GetCrouchTransitionSpeed()
        {
            return crouchSettings.crouchTransitionSpeed;
        }
        
        public void EnableLaserPointer()
        {
            _laserPointer.enabled = true;
        }
    }
}