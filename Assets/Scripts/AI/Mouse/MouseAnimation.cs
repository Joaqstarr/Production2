using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace AI.Mouse
{
    public class MouseAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _head;

        [SerializeField] private Transform _body;

        [SerializeField] private float _rotationSpeed = 600;
        [SerializeField] private float _maxLean = 20f;
        [SerializeField] private float _leanSpeed = 1;

        private NavMeshAgent _navAgent;



        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            HandleBodyRotation();


            Vector3 vel = _navAgent.velocity;

            float curSpeedSqr = vel.sqrMagnitude;
            float maxSpeedSqr = Mathf.Pow(_navAgent.speed, 2);

            float speedInterp = curSpeedSqr / maxSpeedSqr;


            Vector3 desiredLean = Vector3.right * _maxLean * speedInterp;

            _head.localEulerAngles = Vector3.Lerp(_head.localEulerAngles, desiredLean, Time.deltaTime * _leanSpeed);



        }


        private void HandleBodyRotation()
        {
            Vector3 vel = _navAgent.velocity;

            Vector3 rotationVector = Vector3.Cross(vel.normalized, transform.up);

            if (rotationVector.AlmostZero())
            {
                return;
            }

            Vector3 localRotVector = _body.InverseTransformDirection(rotationVector);
            _body.Rotate(localRotVector * -_rotationSpeed * Time.deltaTime);
        }
    }
}