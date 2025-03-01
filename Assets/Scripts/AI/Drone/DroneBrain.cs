using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace AI.Drone
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class DroneBrain : MonoBehaviour
    {
        private DroneStateMachineManager _droneStateMachine;

        [field: SerializeField]
        public Transform[] PatrolPoints { get; private set; } // Assign patrol points in the Inspector

        public NavMeshAgent Agent { get; private set; }

        [SerializeField] private Transform droneObject;
        public float droneRotation = 15f;

        private bool _hasMoved = false;
        private Quaternion _initialRotation;

        [field: SerializeField] public float _lookRadius { get; private set; } = 10;
        [field:SerializeField]
        public Transform _lookTransform { get; private set; }

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _droneStateMachine = new DroneStateMachineManager(this);
        }

        private void Update()
        {
            if (_droneStateMachine != null)
            {
                _droneStateMachine.OnUpdateState();
            }

            Movement();
        }

        private void FixedUpdate()
        {
            if (_droneStateMachine != null)
            {
                _droneStateMachine.OnFixedUpdateState();
            }
        }

        private void Movement()
        {
            bool isMoving = Agent.velocity.magnitude > 0.1f; 

            if (isMoving)
            {
                _hasMoved = true;
                RotateDrone(Agent.velocity.normalized);
            }
            else if (!isMoving && _hasMoved)
            {
                _hasMoved = false;
                ResetRotation();
            }
        }

        private void RotateDrone(Vector3 movementDirection)
        {
            if (movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

                targetRotation = Quaternion.Euler(droneRotation, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

                droneObject.DORotateQuaternion(targetRotation, 0.5f).SetEase(Ease.OutQuad);
            }
        }
        private void ResetRotation()
        {
            droneObject.DORotateQuaternion(_initialRotation, 0.5f).SetEase(Ease.OutQuad);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < PatrolPoints.Length; i++)
            {
                Gizmos.DrawWireSphere(PatrolPoints[i].position, 0.6f);
            }
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _lookRadius);
        }
        
    }
}