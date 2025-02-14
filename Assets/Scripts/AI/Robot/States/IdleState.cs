using AI.Sensing;

namespace AI.Robot.States
{
    public class IdleState : BaseState
    {
        private SenseAlarmListener _alarmListener;
        
        public IdleState(RobotBrain robotBrain, RobotStateMachineManager robotStateMachine) : base(robotBrain, robotStateMachine)
        {
            _alarmListener = new SenseAlarmListener();
        }
        
        
        protected override void OnEnterState()
        {
            base.OnEnterState();
            _alarmListener.OnNotificationReceived += OnAlarmHeard;

        }

        protected override void OnExitState()
        {
            base.OnExitState();
            _alarmListener.OnNotificationReceived -= OnAlarmHeard;

        }
        
        private void OnAlarmHeard(SenseNotificationContext notification)
        {
            _robotStateMachine.SwitchToFollowState();
        }

    }
}