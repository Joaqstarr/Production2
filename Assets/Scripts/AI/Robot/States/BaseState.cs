using UnityEngine;
using Utility.StateMachine;

namespace AI.Robot.States
{
    public abstract class BaseState : HierarchalStateMachine
    {
        protected RobotBrain _robotBrain;
        protected RobotStateMachineManager _robotStateMachine;
        protected GameObject _gameObject;
        protected Transform _transform;

        public BaseState(RobotBrain robotBrain)
        {
            SetupRobotBrain(robotBrain);
        }
        public BaseState(RobotBrain robotBrain, RobotStateMachineManager robotStateMachine)
        {
            SetupRobotBrain(robotBrain);
            _robotStateMachine = robotStateMachine;
        }
        private void SetupRobotBrain(RobotBrain robotBrain)
        {
            _robotBrain = robotBrain;
            _gameObject = _robotBrain.gameObject;
            _transform = _robotBrain.transform;
        }
    }
}