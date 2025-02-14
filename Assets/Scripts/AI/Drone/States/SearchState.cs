using AI.Sensing;

namespace AI.Drone.States
{
    public class SearchState : BaseState
    {
        private SightSenseListener _sightSensor;
        public SearchState(DroneBrain brain, DroneStateMachineManager manager) : base(brain, manager)
        {
            _sightSensor = new SightSenseListener(_gameObject, 10);
            //SightSensor += OnSightViewed;
            
            _sightSensor.OnNotificationReceived += OnSightReceived;
        }

        private void OnSightReceived(SenseNotificationContext notification)
        {
            _drone.Agent.SetDestination(notification.Position);
        }

        
    }
}