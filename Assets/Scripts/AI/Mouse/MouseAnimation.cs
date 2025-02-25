using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace AI.Mouse
{
    public class MouseAnimation : MonoBehaviour
    {
        [SerializeField]
        private Transform _body;
        [SerializeField]
        private float _rotationSpeed;
        private NavMeshAgent _navAgent;
        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            HandleBodyRotation();

        }

        private void HandleBodyRotation()
        {
            Vector3 vel = _navAgent.velocity.normalized;

            Vector3 rotationVector = Vector3.Cross(vel, transform.up);

            Vector3 localRotVector = _body.InverseTransformDirection(rotationVector);
            _body.Rotate(localRotVector * -_rotationSpeed * Time.deltaTime);
        }
    }
}