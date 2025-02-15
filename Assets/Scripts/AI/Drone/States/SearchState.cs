using AI.Drone.States.SearchStates;
using AI.Sensing;

namespace AI.Drone.States
{
    public class SearchState : BaseState
    {
        #region States
        
        private BaseState _patrolState;
        private BaseState _lookState;

        #endregion
        private SightSenseListener _sightSensor;
        public SearchState(DroneBrain brain, DroneStateMachineManager manager) : base(brain, manager)
        {
            _sightSensor = new SightSenseListener(_gameObject, 10);
            _patrolState = new PatrolState(brain, manager, this);
            _lookState = new LookState(brain, manager, this);
        }

        private void OnSightReceived(SenseNotificationContext notification)
        {
            //_drone.Agent.SetDestination(notification.Position);
            SenseNotificationSubsystem.TriggerAlarmNotification(notification.Position);
            SwitchToLookState();
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            
            _sightSensor.OnNotificationReceived += OnSightReceived;

            SwitchToPatrolState();
        }

        public void SwitchToPatrolState()
        {
            SwitchState(_patrolState);
        }
        public void SwitchToLookState()
        {
            SwitchState(_lookState);
        }
        protected override void OnExitState()
        {
            base.OnExitState();
            
            _sightSensor.OnNotificationReceived -= OnSightReceived;
        }
    }
}