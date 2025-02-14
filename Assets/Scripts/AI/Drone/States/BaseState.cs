using UnityEngine;
using Utility.StateMachine;

namespace AI.Drone.States
{
    public abstract class BaseState : HierarchalStateMachine
    {
        protected GameObject _gameObject;
        protected Transform _transform;
        protected DroneBrain _drone;
        private DroneStateMachineManager _stateManager;
        
        public BaseState(DroneBrain drone, DroneStateMachineManager manager)
        {
            SetupDroneBrain(drone);
            _stateManager = manager;
        }
        
        public BaseState(DroneBrain drone)
        {
            SetupDroneBrain(drone);
        }
        
        private void SetupDroneBrain(DroneBrain drone)
        {
            _gameObject = drone.gameObject;
            _transform = _gameObject.transform;
            _drone = drone;
        }
    }
}