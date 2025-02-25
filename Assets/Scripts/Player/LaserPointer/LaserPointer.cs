using System;
using AI.Sensing;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.LaserPointer
{
    public class LaserPointer : MonoBehaviour
    {
        private static readonly int ClickLaserProperty = Animator.StringToHash("ClickLaser");
        private static readonly int IsHoldingLaserPointer = Animator.StringToHash("IsHoldingLaserPointer");
        private PlayerControls _controls;

        [SerializeField]
        private Transform _lookTransform;
        [SerializeField]
        private TwoBoneIKConstraint _laserHoldConstraint;

        [SerializeField] private LaserBeam _laserPointerObject;
        private Animator _animator;

        [SerializeField] private LayerMask _laserHitMask;

        private void Awake()
        {
            _controls = GetComponent<PlayerControls>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _laserPointerObject.gameObject.SetActive(true);
            
            _laserHoldConstraint.weight = 1;

            _animator.SetBool(IsHoldingLaserPointer, true);
            _controls.OnLaserPressed += OnLaserPressed;
            _controls.OnLaserUnpressed += OnLaserUnpressed;
        }

        private void OnDisable()
        {
            _laserPointerObject.gameObject.SetActive(false);

            _animator.SetBool(IsHoldingLaserPointer, false);
            _laserHoldConstraint.weight = 0;
            _controls.OnLaserPressed -= OnLaserPressed;
            _controls.OnLaserUnpressed -= OnLaserUnpressed;
        }

        // Start is called before the first frame update
        private void OnLaserUnpressed()
        {
            _animator.SetTrigger(ClickLaserProperty);
            _laserPointerObject.DisableBeam();

        }

        private void OnLaserPressed()
        {
            _laserPointerObject.EnableBeam();
            
            _animator.SetTrigger(ClickLaserProperty);
        }

        // Update is called once per frame
        void Update()
        {
            if (_controls.IsLaserOn)
            {
                RaycastHit hit;

                if (Physics.Raycast(_lookTransform.position, _lookTransform.forward, out hit, 100, _laserHitMask))
                {
                    SenseNotificationSubsystem.TriggerLaserNotification(hit.point);
                    Debug.DrawLine(_lookTransform.position + _lookTransform.right, hit.point, Color.red);
                }
            }
        }
    }
}
