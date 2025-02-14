using System;
using UnityEngine;

namespace AI.Drone
{
    public class DroneBrain : MonoBehaviour
    {
        private DroneStateMachineManager _droneStateMachine;

        private void Awake()
        {
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