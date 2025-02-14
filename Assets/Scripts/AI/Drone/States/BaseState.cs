using Utility.StateMachine;

namespace AI.Drone.States
{
    public abstract class BaseState : HierarchalStateMachine
    {
        protected DroneBrain _drone;
        private DroneStateMachineManager _stateManager;
        
        public BaseState(DroneBrain drone, DroneStateMachineManager manager)
        {
            _drone = drone;
            _stateManager = manager;
        }

        public BaseState(DroneBrain drone)
        {
            _drone = drone;
        }

    }
}