using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Drone
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class DroneBrain : MonoBehaviour
    {
        private DroneStateMachineManager _droneStateMachine;

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

    }
}