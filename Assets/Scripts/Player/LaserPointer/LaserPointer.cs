using System;
using AI.Sensing;
using UnityEngine;

namespace Player.LaserPointer
{
    public class LaserPointer : MonoBehaviour
    {
        private PlayerControls _controls;

        [SerializeField]
        private Transform _lookTransform;

        [SerializeField] private LayerMask _laserHitMask;
        private void OnEnable()
        {
            _controls = GetComponent<PlayerControls>();

            _controls.OnLaserPressed += OnLaserPressed;
            _controls.OnLaserUnpressed += OnLaserUnpressed;
        }

        private void OnDisable()
        {
            _controls.OnLaserPressed -= OnLaserPressed;
            _controls.OnLaserUnpressed -= OnLaserUnpressed;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        private void OnLaserUnpressed()
        {
        }

        private void OnLaserPressed()
        {
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
