using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Drone
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class DroneBrain : MonoBehaviour
    {
        private DroneStateMachineManager _droneStateMachine;

        [field: SerializeField]
        public Transform[] PatrolPoints { get; private set; } // Assign patrol points in the Inspector

        public NavMeshAgent Agent { get; private set; }
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
        }

        private void FixedUpdate()
        {
            if (_droneStateMachine != null)
            {
                _droneStateMachine.OnFixedUpdateState();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < PatrolPoints.Length; i++)
            {
                Gizmos.DrawWireSphere(PatrolPoints[i].position, 0.6f);
            }
        }
    }
}