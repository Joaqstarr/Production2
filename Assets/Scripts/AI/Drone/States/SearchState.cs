using AI.Drone.States.SearchStates;
using AI.Sensing;
using UnityEngine;

namespace AI.Drone.States
{
    public class SearchState : BaseState
    {
        public float TimeoutTimer { get; private set; } = 0;
        #region States
        
        private BaseState _patrolState;
        private BaseState _lookState;
        private BaseState _lostState;
        #endregion
        private SightSenseListener _sightSensor;
        public SearchState(DroneBrain brain, DroneStateMachineManager manager) : base(brain, manager)
        {
            _sightSensor = new SightSenseListener(_drone.LookPoint.gameObject, brain._lookRadius, brain.EnvLayers);
            _patrolState = new PatrolState(brain, manager, this);
            _lookState = new LookState(brain, manager, this);
            _lostState = new LostState(brain, manager, this);
        }

        private void OnSightReceived(SenseNotificationContext notification)
        {
            //_drone.Agent.SetDestination(notification.Position);
            
            SenseNotificationSubsystem.TriggerAlarmNotification(notification.Position, _drone.AlertGroupID);

            _drone._lookTransform.position = notification.Position;

            TimeoutTimer = 2f;
            SwitchToLookState();
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
            
            TimeoutTimer -= Time.deltaTime;
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

        public void SwitchToLostState()
        {
            SwitchState(_lostState);
        }
        protected override void OnExitState()
        {
            base.OnExitState();
            
            _sightSensor.OnNotificationReceived -= OnSightReceived;
        }
    }
}