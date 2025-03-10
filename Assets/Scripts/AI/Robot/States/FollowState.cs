using AI.Sensing;

namespace AI.Robot.States
{
    public class FollowState : BaseState
    {
        private SenseAlarmListener _alarmListener;

        public FollowState(RobotBrain robotBrain, RobotStateMachineManager robotStateMachine) : base(robotBrain, robotStateMachine)
        {
            _alarmListener = new SenseAlarmListener(robotBrain.AlarmGroupID);

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

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (_robotBrain.Agent.remainingDistance < 0.4f)
            {
                _robotStateMachine.SwitchToIdleState();
            }
        }

        private void OnAlarmHeard(SenseNotificationContext notification)
        {
            if (_robotBrain != null && _robotBrain.enabled && _robotBrain.Agent.enabled)
            {
                _robotBrain.Agent.SetDestination(notification.Position);
            }
        }
    }
}